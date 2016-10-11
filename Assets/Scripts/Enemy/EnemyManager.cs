﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Zenject;
using ModestTree;

public class EnemyManager
{
	
	readonly List<Enemy> _enemyList = new List<Enemy>();
	float _spawnTime;
	float _spawnCounter;
		

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
		_spawnTime = _settings.SpawnTimeInitial;
		_spawnCounter = _spawnTime;
	}


	public void Stop()
	{
	}


	public void Tick(float playerSpeed)
	{
		for(int i=0; i < _enemyList.Count ;)
		{
			_enemyList[i].Tick(playerSpeed);
			if(CheckDispose(_enemyList[i]))
			{
				_enemyList[i].Dispose();
				_enemyList.RemoveAt(i);
			}
			else
				i++;
		}
		Spawn();
	}


	bool CheckDispose(Enemy enemy)
	{
		return enemy.transform.position.x < _level.Left;
	}


	void Spawn()
	{
		_spawnCounter -= Time.deltaTime;
		if(_spawnCounter <= 0)
		{
			Enemy enemy = _enemyFactory.CreateEnemy(EnemyTypes.Cactus);
			enemy.transform.position = _spawnPoint.position;
			enemy.transform.SetParent(_spawnPoint);
			_enemyList.Add(enemy);

			_spawnCounter = _spawnTime;
			if(_spawnTime > _settings.SpawnTimeMinumum)
			{
				_spawnTime -= _settings.SpawnTimeReduct;
				if(_spawnTime < _settings.SpawnTimeMinumum)
					_spawnTime = _settings.SpawnTimeMinumum;
			}
		}
	}


	[Serializable]
	public class Settings
	{
		public float SpawnTimeInitial;
		public float SpawnTimeReduct;
		public float SpawnTimeMinumum;
	}
}
