using UnityEngine;
using System.Collections;
using System;
using Zenject;
using ModestTree;

public class Cactus : Enemy
{
	Player.Settings _playerSettings;

	[Inject]
	public void Construct(
		Player.Settings playerSettings
	)
	{
		_playerSettings = playerSettings;
	}

	public override void Tick()
	{
		transform.Translate(new Vector3(-_playerSettings.MoveSpeed, 0, 0) * Time.deltaTime);
	}

	public override void Dispose()
	{
		GameObject.Destroy(gameObject);
	}

	public class Factory : Factory<Cactus>
	{
	}
}
