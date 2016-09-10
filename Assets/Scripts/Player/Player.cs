using UnityEngine;
using System.Collections;
using Zenject;
using System;

public class Player : MonoBehaviour 
{
	PlayerStateFactory _stateFactory;
	PlayerState _state = null;

	[Inject]
	public void Construct(PlayerStateFactory stateFactory)
	{
		_stateFactory = stateFactory;
	}

	public void Start()
	{
		ChangeState(PlayerStates.Running);
	}

	public void Update()
	{
		_state.Update();
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

	[Serializable]
	public class Settings
	{
		public float MoveSpeed;
	}
}
