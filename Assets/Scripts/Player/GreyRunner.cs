using UnityEngine;
using System.Collections;
using Zenject;
using System;
using ModestTree;

public class GreyRunner : MonoBehaviour , IRunner
{
	[InjectOptional(Id = "runner_container")] GameObject Container;

	public enum PlayerStates
	{
		OnGround,
		Jumping,
	}

	PlayerStates _state;

	Vector3 _originalPosition;
	Signals.PlayerDead.Trigger _deadTrigger;
	IRunnerSettings _settings;
	IEquipment _weapon;

	public float CurrentSpeed
	{	
		get; set;
	}

	[Inject]
	public void Construct(
		Signals.PlayerDead.Trigger deadTrigger,
		IRunnerSettings settings,
		IEquipment weapon
	)
	{
		_deadTrigger = deadTrigger;
		_settings = settings;
		_weapon = weapon;

		_state = PlayerStates.OnGround;

		_originalPosition = transform.position;
	}

	public void Initialize()
	{
		if(Container != null)
			transform.SetParent(Container.transform);

		CurrentSpeed = _settings.InitialSpeed;
		transform.position = _originalPosition;
		_state = PlayerStates.OnGround;
		_weapon.Initialize();
	}

	public void Tick()
	{
		UpdatePlayer();
		_weapon.Tick();
	}

	public void Stop()
	{
		CurrentSpeed = 0;
	}
		
	#region PlayerStateUpdate

	void UpdatePlayer()
	{
		Assert.That(_state != null);
		switch(_state)
		{
			case PlayerStates.OnGround:
			{
				UpdateOnGround();
				break;
			}

			case PlayerStates.Jumping:
			{
				UpdateJumping();
				break;
			}

			default:
			{
				Assert.That(false);
				break;	
			}
		}	
	}

	void UpdateOnGround()
	{
		Assert.That(_state == PlayerStates.OnGround);
		Acceleration();
	}

	void UpdateJumping()
	{
		Assert.That(_state == PlayerStates.Jumping);
		Acceleration();

		Assert.That(_jumpForce.x == 0 && _jumpForce.z == 0);
		transform.Translate(_jumpForce * Time.deltaTime);
		_jumpForce.y -= _settings.Gravity;

		if(ReachGround())
		{
			transform.position = new Vector3(transform.position.x, _originalPosition.y, transform.position.y);
			_state = PlayerStates.OnGround;
		}
	}
	#endregion
		
	#region Jump
	bool _jumpHolding;
	float _jumpHoldTime;
	Vector3 _jumpForce;

	public void StartAction()
	{
		if(_state == PlayerStates.OnGround)
		{
			Assert.That(!_jumpHolding);
			_jumpHolding = true;
			_jumpHoldTime = 0;
		}
	}

	public void HoldAction()
	{
		if(_state == PlayerStates.OnGround && _jumpHolding)
		{
			_jumpHoldTime += Time.deltaTime;
			if(_jumpHoldTime >= _settings.MaxJumpHoldTime)
			{
				Jump();
			}
		}
	}

	public void StopAction()
	{
		if(_state == PlayerStates.OnGround && _jumpHolding)
		{
			Jump();
		}
	}

	public void Jump()
	{
		Assert.That(_state == PlayerStates.OnGround);
		float jumpForce = 0;
		if(_jumpHoldTime < _settings.MinJumpHoldTime)
		{
			jumpForce = _settings.MinJumpForce;
		}
		else
		{
			float holdRatio = (_jumpHoldTime - _settings.MinJumpHoldTime) / (_settings.MaxJumpHoldTime - _settings.MinJumpHoldTime);
			jumpForce = Math.Min(holdRatio * (_settings.MaxJumpForce - _settings.MinJumpForce) + _settings.MinJumpForce, _settings.MaxJumpForce);
		}

		_state = PlayerStates.Jumping;
		_jumpHolding = false;
		_jumpForce = new Vector3(0, jumpForce, 0);

		#if DEBUG
		Debugger.instance.UpdatePlayerJump(jumpForce);
		#endif
	}
		
	bool ReachGround()
	{
		return transform.position.y <= _originalPosition.y;
	}
	#endregion

	void Acceleration()
	{
		if(CurrentSpeed < _settings.MaximumSpeed)
		{
			CurrentSpeed += _settings.Acceleration;
			if(CurrentSpeed > _settings.MaximumSpeed)
			{
				CurrentSpeed = _settings.MaximumSpeed;
			}
		}
			
		Assert.That(CurrentSpeed <= _settings.MaximumSpeed);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<Enemy>() != null)
		{
			_deadTrigger.Fire();
		}
	}
}
