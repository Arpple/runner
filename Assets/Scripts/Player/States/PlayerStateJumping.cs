using UnityEngine;
using System.Collections;
using System;
using Zenject;

public class PlayerStateJumping : PlayerState
{
	[Serializable]
	public class Settings
	{
		public float JumpSpeed;
		public float Gravity;
	}

	readonly Settings _settings;
	readonly Player _player;

	Vector3 _groundPosition;
	Vector3 _movement;

	public PlayerStateJumping(
		Settings settings,
		Player player
	)
	{
		_settings = settings;
		_player = player;
	}

	public override void Start()
	{
		_groundPosition = _player.transform.position;
		_movement.y = _settings.JumpSpeed;
	}

	public override void Update()
	{
		Move();
		if(ReachGround())
		{
			_player.transform.position = _groundPosition;
			_player.ChangeState(PlayerStates.Running);
		}
	}
		
	public override void Stop()
	{
		
	}

	void Move()
	{
		_player.transform.Translate(_movement * Time.deltaTime);
		_movement.y -= _settings.Gravity;
	}

	bool ReachGround()
	{
		return _player.transform.position.y <= _groundPosition.y;
	}

	public class Factory : Factory<PlayerStateJumping>
	{
	}
}
