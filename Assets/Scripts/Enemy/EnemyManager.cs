using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Zenject;
using ModestTree;

public class EnemyManager
{
	
	readonly List<Enemy> _enemyList = new List<Enemy>();


	readonly Settings _settings;
	readonly EnemyFactory _enemyFactory;
	readonly LevelHelper _level;
	readonly Transform _spawnPoint;


	public EnemyManager(
		Settings settings,
		EnemyFactory enemyFactory,
		LevelHelper level,
		[Inject(Id = "Spawner")]Transform spawnPoint
	)
	{
		_settings = settings;
		_enemyFactory = enemyFactory;
		_level = level;
		_spawnPoint = spawnPoint;
	}


	public void Initialize()
	{
		ResetAll();
	}


	void ResetAll()
	{
		foreach(Enemy enemy in _enemyList)
		{
			enemy.Dispose();
		}
		_enemyList.Clear();
	}


	public void Stop()
	{
	}


	public void Tick(float playerSpeed)
	{
		if(_enemyList.Count < _settings.MaxEnemy)
		{
			//Spawn new enemy
			Enemy enemy = _enemyFactory.CreateEnemy(EnemyTypes.Cactus);
			enemy.transform.SetParent(_spawnPoint);

			if(_enemyList.Count > 0)
			{
				Vector3 lastPosition = _enemyList.LastOrDefault().transform.position;
				enemy.transform.position = new Vector3(lastPosition.x + GetGap(playerSpeed), lastPosition.y, lastPosition.z);
			}
			else
			{
				enemy.transform.position = _spawnPoint.position;
			}
			_enemyList.Add(enemy);
		}

		if(_enemyList.Count > 0)
		{
			_enemyList.ForEach(enemy => enemy.Tick(playerSpeed));

			if(CheckDispose(_enemyList.First()))
			{
				_enemyList[0].Dispose();
				_enemyList.RemoveAt(0);	
			}
		}
	}


	bool CheckDispose(Enemy enemy)
	{
		return enemy.transform.position.x < _level.Left;
	}

	float GetGap(float speed)
	{
		float minGap = _settings.MinGapInitial * _settings.GapCofactor + speed;
		float maxGap = _settings.MaxGapInitial * _settings.GapCofactor + speed;
		return UnityEngine.Random.Range(minGap, maxGap);
	}


	void Spawn()
	{
	}

	[Serializable]
	public class Settings
	{
		public int MaxEnemy;
		public float MinGapInitial;
		public float MaxGapInitial;
		public float GapCofactor;
	}
}
