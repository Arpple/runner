using UnityEngine;
using System.Collections;
using Zenject;
using System;
using ModestTree;

public class GreyRunner : MonoBehaviour , IRunner
{
	public IRunnerSettings Setting;

	[InjectOptional(Id = "runner_container")] GameObject Container;

	public enum PlayerStates
	{
		OnGround,
		Jumping,
	}

	PlayerStates _state;

	Vector3 _originalPosition;
	Signals.PlayerDead.Trigger _deadTrigger;
	[InjectOptional] IEquipment _weapon;

	public float CurrentSpeed
	{	
		get; set;
	}

	[Inject]
	public void Construct(
		Signals.PlayerDead.Trigger deadTrigger
	)
	{
		_deadTrigger = deadTrigger;
		_state = PlayerStates.OnGround;

		_originalPosition = transform.position;
	}

	public void Initialize()
	{
		if(Container != null)
			transform.SetParent(Container.transform);

		CurrentSpeed = Setting.InitialSpeed;
		transform.position = _originalPosition;
		_state = PlayerStates.OnGround;
		if(_weapon != null)
			_weapon.Initialize();
	}

	public void Tick()
	{
		UpdatePlayer();
		if(_weapon != null)
			_weapon.Tick();
	}

	public void Stop()
	{
		CurrentSpeed = 0;
	}
		
	#region PlayerStateUpdate

	void UpdatePlayer()
	{
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
				Assert.That(false, "Undefine state");
				break;	
			}
		}	
	}

	void UpdateOnGround()
	{
		Assert.That(_state == PlayerStates.OnGround, "wrong state update");
		Acceleration();
	}

	void UpdateJumping()
	{
		Assert.That(_state == PlayerStates.Jumping, "wrong state update");
		Acceleration();

		Assert.That(_jumpForce.x == 0 && _jumpForce.z == 0, "jump force not y-axis");
		transform.Translate(_jumpForce * Time.deltaTime);
		_jumpForce.y -= Setting.Gravity;

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
			if(_jumpHoldTime >= Setting.MaxJumpHoldTime)
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
		if(_jumpHoldTime < Setting.MinJumpHoldTime)
		{
			jumpForce = Setting.MinJumpForce;
		}
		else
		{
			float holdRatio = (_jumpHoldTime - Setting.MinJumpHoldTime) / (Setting.MaxJumpHoldTime - Setting.MinJumpHoldTime);
			jumpForce = Math.Min(holdRatio * (Setting.MaxJumpForce - Setting.MinJumpForce) + Setting.MinJumpForce, Setting.MaxJumpForce);
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
		if(CurrentSpeed < Setting.MaximumSpeed)
		{
			CurrentSpeed += Setting.Acceleration;
			if(CurrentSpeed > Setting.MaximumSpeed)
			{
				CurrentSpeed = Setting.MaximumSpeed;
			}
		}
			
		Assert.That(CurrentSpeed <= Setting.MaximumSpeed, "Speed exceed maximum");
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.GetComponent<Enemy>() != null)
		{
			_deadTrigger.Fire();
		}
	}
}
