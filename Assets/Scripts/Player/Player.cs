using UnityEngine;
using System.Collections;
using Zenject;
using System;
using ModestTree;

public class Player : MonoBehaviour 
{
	public enum PlayerStates
	{
		OnGround,
		Jumping,
	}

	PlayerStates _state;

	Vector3 _originalPosition;
	Signals.PlayerDead.Trigger _deadTrigger;
	Settings _settings;

	public float CurrentSpeed
	{
		get; private set;
	}


	public float currentJump;

	[Inject]
	public void Construct(
		Signals.PlayerDead.Trigger deadTrigger,
		Settings settings
	)
	{
		_deadTrigger = deadTrigger;
		_settings = settings;

		_state = PlayerStates.OnGround;

		_originalPosition = transform.position;
	}

	public void Initialize()
	{
		CurrentSpeed = _settings.InitialSpeed;
		transform.position = _originalPosition;
		_state = PlayerStates.OnGround;
	}

	public void Tick()
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
				Assert.That(false);
				break;	
			}
		}
	}

	Vector3 _jumpForce;

	public void Jump()
	{
		if(_state == PlayerStates.OnGround)
		{
			_jumpForce = new Vector3(0, _settings.JumpForce, 0);
			_state = PlayerStates.Jumping;

			#if DEBUG
			Debugger.instance.UpdatePlayerJump(_jumpForce.y);
			#endif
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

	bool ReachGround()
	{
		return transform.position.y <= _originalPosition.y;
	}

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

	[Serializable]
	public class Settings
	{
		public float InitialSpeed;
		public float Acceleration;
		public float MaximumSpeed;

		public float JumpForce;
		public float Gravity;
	}
}
