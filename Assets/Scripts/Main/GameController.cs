using UnityEngine;
using System;
using System.Collections;
using Zenject;
using ModestTree;

public class GameController : IInitializable, ITickable, IDisposable
{
	readonly Player _player;
	readonly EnemyManager _enemyManager;

	public GameController(
		Player player,
		EnemyManager enemyManager
	)
	{
		_player = player;
		_enemyManager = enemyManager;
	}

	public void Initialize()
	{
		_enemyManager.Start();
	}

	public void Tick()
	{}

	public void Dispose()
	{}
}
