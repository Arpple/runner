using UnityEngine;
using System.Collections;
using Zenject;
using System;

public class PlayerStateRunning : PlayerState
{

	readonly Player _player;

	public PlayerStateRunning(
		Player player
	)
	{
		_player = player;
	}

	public override void Update()
	{
		if(Input.GetMouseButton(0))
		{
			_player.ChangeState(PlayerStates.Jumping);
		}	
	}

	public class Factory : Factory<PlayerStateRunning>
	{
	}

}
