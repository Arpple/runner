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
	Background _background;

	[Inject]
	public void Construct(
		PlayerStateFactory stateFactory,
		Signals.PlayerDead.Trigger deadTrigger,
		Background background
	)
	{
		_stateFactory = stateFactory;
		_deadTrigger = deadTrigger;
		_background = background;

		_originalPosition = transform.position;
	}

	public void Initialize()
	{
		transform.position = _originalPosition;
		ChangeState(PlayerStates.Running);
		_background.Reset();
	}

	public void Tick()
	{
		_state.Update();
		_background.Tick();
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
		public float MoveSpeed;
	}
}
