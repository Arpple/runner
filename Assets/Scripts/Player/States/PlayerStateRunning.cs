using UnityEngine;
using System.Collections;
using Zenject;

public class PlayerStateRunning : PlayerState
{
//	public class Settings
//	{
//	}

//	readonly Settings _settings;
	readonly Player _player;

	public PlayerStateRunning(
		Player player
//		Settings settings
	)
	{
		_player = player;
//		_settings = settings;
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
