﻿using UnityEngine;
using System.Collections;
using System;
using Zenject;

public class PlayerStateJumping : PlayerState
{

	Vector3 _groundPosition;
	Vector3 _movement;
	
	readonly Settings _settings;
	readonly Player _player;


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
		_movement.y = _player.currentJump;
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


	[Serializable]
	public class Settings
	{
		public float Gravity;
	}

	public class Factory : Factory<PlayerStateJumping>
	{
	}
}
