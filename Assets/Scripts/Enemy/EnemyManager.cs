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
    readonly List<Enemy> _ghostyList = new List<Enemy>();

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
        //TODO : SpawnerGhosty
        SpawnGhosty(playerSpeed);

        //TODO : Spawn Enemy
		if(_enemyList.Count < _settings.MaxEnemy)
		{
			//Spawn new enemy
			Enemy enemy = _enemyFactory.CreateEnemy(RandomEnemy());
			enemy.Initialize();

			if(_enemyList.Count > 0)
			{
				Vector3 lastPosition = _enemyList.LastOrDefault().transform.position;
				enemy.transform.position = new Vector3(lastPosition.x + GetGap(playerSpeed), enemy.transform.position.y, enemy.transform.position.z);
			}
			else
			{
				enemy.transform.position = new Vector3(_spawnPoint.position.x, enemy.transform.position.y, enemy.transform.position.z);
			}
			_enemyList.Add(enemy);
		}

		if(_enemyList.Count > 0)
		{
			var deadEnemies = new List<Enemy>();
			_enemyList.ForEach(enemy => {
				enemy.Tick(playerSpeed);
				if(CheckDispose(enemy))
				{
					deadEnemies.Add(enemy);
				}
			});

			deadEnemies.ForEach(e => {
				e.Dispose();
				_enemyList.Remove(e);
			});
		}
	}

	EnemyTypes RandomEnemy()
	{
		Array values = Enum.GetValues(typeof(EnemyTypes));
		return (EnemyTypes)values.GetValue(UnityEngine.Random.Range(0, values.Length));
	}


	bool CheckDispose(Enemy enemy)
	{
		return enemy.transform.position.x < _level.Left || enemy.IsDead();
	}

	float GetGap(float speed)
	{
		float minGap = _settings.MinGapInitial * _settings.GapCofactor + speed;
		float maxGap = _settings.MaxGapInitial * _settings.GapCofactor + speed;
		return UnityEngine.Random.Range(minGap, maxGap);
	}

    private float _ghostyCooldown = 2.0f; // CD = 1.0-3.0f
    private void SpawnGhosty(float playerSpeed) {
        _ghostyCooldown -= Time.deltaTime;

        if (_ghostyCooldown < 0f && UnityEngine.Random.Range(0, 100) >= 90f && _ghostyList.Count < 10) {
            Enemy ghosty = _enemyFactory.CreateEnemy(EnemyTypes.Ghosty);
            ghosty.Initialize();
            float y_position = UnityEngine.Random.Range(-4f,4f)
                , x_position = UnityEngine.Random.Range(-GetGap(playerSpeed), GetGap(playerSpeed));
            if (_ghostyList.Count > 0) {
                Vector3 lastPosition = _ghostyList.LastOrDefault().transform.position;
                ghosty.transform.position = new Vector3(lastPosition.x +x_position, ghosty.transform.position.y+y_position, ghosty.transform.position.z);
            } else {
                ghosty.transform.position = new Vector3(_spawnPoint.position.x, ghosty.transform.position.y+y_position, ghosty.transform.position.z);
            }
            _ghostyList.Add(ghosty);
            _ghostyCooldown = UnityEngine.Random.Range(1f,3f);
        }

        if (_ghostyList.Count > 0) {
            var deadEnemies = new List<Enemy>();
            _ghostyList.ForEach(enemy => {
                enemy.Tick(playerSpeed);
                if (CheckDispose(enemy)) {
                    deadEnemies.Add(enemy);
                }
            });

            deadEnemies.ForEach(e => {
                e.Dispose();
                _ghostyList.Remove(e);
            });
        }

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
