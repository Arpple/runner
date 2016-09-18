using UnityEngine;
using System.Collections;
using Zenject;
using System;
using ModestTree;

public class PlayerStateRunning : PlayerState
{

	float _currentJumpHoldTime;
	bool _jumpHolding;

	readonly Player _player;
	readonly Settings _settings;

	public PlayerStateRunning(
		Player player,
		Settings settings
	)
	{
		_player = player;
		_settings = settings;
	}
		
	public override void Start()
	{
		_player.currentJump = _settings.MinJump;
		_currentJumpHoldTime = 0;
		_jumpHolding = false;
	}

	public override void Update()
	{
		//start holding
		if(Input.GetMouseButtonDown(0))
		{
			Assert.That(!_jumpHolding);
			_currentJumpHoldTime = 0;
			_jumpHolding = true;
		}

		//holding
		if(Input.GetMouseButton(0) && _jumpHolding)
		{
			_currentJumpHoldTime += Time.deltaTime;
			if(_currentJumpHoldTime >= _settings.MaxJumpHoldTime)
			{
				Jump();
			}
		}

		//end holding
		if(Input.GetMouseButtonUp(0) && _jumpHolding)
		{
			Jump();
		}
	}

	void Jump()
	{
		if(_currentJumpHoldTime < _settings.MinJumpHoldTime)
		{
			_player.currentJump = _settings.MinJump;
		}
		else
		{
			_player.currentJump = Math.Min(_currentJumpHoldTime / _settings.MaxJumpHoldTime * _settings.MaxJump, _settings.MaxJump);
		}

		#if DEBUG
		Debugger.instance.UpdatePlayerJump(_player.currentJump);
		#endif

		_jumpHolding = false;
		_player.ChangeState(PlayerStates.Jumping);
	}

	[Serializable]
	public class Settings
	{
		public float MinJump;
		public float MinJumpHoldTime;
		public float MaxJump;
		public float MaxJumpHoldTime;
	}

	public class Factory : Factory<PlayerStateRunning>
	{
	}

}
