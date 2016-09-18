using UnityEngine;
using System.Collections;
using Zenject;
using System;

public class Player : MonoBehaviour 
{

	Vector3 _originalPosition;

	PlayerStateFactory _stateFactory;
	PlayerState _state = null;
	Signals.PlayerDead.Trigger _deadTrigger;
	Settings _settings;

	public float currentSpeed;
	public float currentJump;

	[Inject]
	public void Construct(
		PlayerStateFactory stateFactory,
		Signals.PlayerDead.Trigger deadTrigger,
		Settings settings
	)
	{
		_stateFactory = stateFactory;
		_deadTrigger = deadTrigger;
		_settings = settings;

		_originalPosition = transform.position;
	}

	public void Initialize()
	{
		currentSpeed = _settings.InitialSpeed;
		transform.position = _originalPosition;
		ChangeState(PlayerStates.Running);
	}

	public void Tick()
	{
		_state.Update();
		if(currentSpeed < _settings.MaximumSpeed)
		{
			currentSpeed = Math.Min(currentSpeed + _settings.Acceleration * Time.deltaTime, _settings.MaximumSpeed);
		}
	}

	public void ChangeState(PlayerStates state)
	{
		if (_state != null)
		{
			_state.Stop();
		}
		_state = _stateFactory.CreateState(state);
		_state.Start();
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
	}
}
