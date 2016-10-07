using UnityEngine;
using System.Collections;
using System;
using Zenject;
using ModestTree;

public class Cactus : Enemy
{
	Player _player;

	[Inject]
	public void Construct(
		Player player
	)
	{
		_player = player;
	}

	public override void Tick()
	{
		Move();
	}

	void Move()
	{
	}

	public override void Dispose()
	{
		GameObject.Destroy(gameObject);
	}

	public class Factory : Factory<Cactus>
	{
	}
}
