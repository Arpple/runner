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
		transform.Translate(new Vector3(- _player.currentSpeed, 0, 0) * Time.deltaTime);
	}

	public override void Dispose()
	{
		GameObject.Destroy(gameObject);
	}

	public class Factory : Factory<Cactus>
	{
	}
}
