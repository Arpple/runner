using UnityEngine;
using System;
using System.Collections;
using Zenject;
using ModestTree;

public enum GameStates
{
	WaitingToStart,
	Playing,
	GameOver,
}

public class GameController : IInitializable, ITickable, IDisposable
{
	readonly Player _player;
	readonly EnemyManager _enemyManager;
	readonly Signals.PlayerDead _playerDeadSignal;

	GameStates _state = GameStates.WaitingToStart;

	public GameController(
		Player player,
		EnemyManager enemyManager,
		Signals.PlayerDead playerDeadSignal
	)
	{
		_player = player;
		_enemyManager = enemyManager;
		_playerDeadSignal = playerDeadSignal;
	}

	public void Initialize()
	{
		_playerDeadSignal.Event += OnPlayerDead;
	}

	public void Dispose()
	{
		_playerDeadSignal.Event -= OnPlayerDead;
	}

	public void Tick()
	{
		switch(_state)
		{
			case GameStates.WaitingToStart:
			{
				UpdateStarting();
				break;
			}

			case GameStates.Playing:
			{
				UpdatePlaying();
				break;
			}

			case GameStates.GameOver:
			{
				UpdateGameOver();
				break;
			}

			default:
			{
				Assert.That(false);
				break;
			}
		
		}
	}

	void UpdateStarting()
	{
		Assert.That(_state == GameStates.WaitingToStart);
		if(Input.GetMouseButton(0))
		{
			StartGame();
		}
	}

	void UpdatePlaying()
	{
		Assert.That(_state == GameStates.Playing);
		_player.Tick();
		_enemyManager.Tick();
	}

	void UpdateGameOver()
	{
		Assert.That(_state == GameStates.GameOver);
		if(Input.GetMouseButton(0))
		{
			StartGame();
		}
	}

	void StartGame()
	{
		Assert.That(_state == GameStates.WaitingToStart || _state == GameStates.GameOver);
		_state = GameStates.Playing;
		_player.Initialize();
		_enemyManager.Start();
	}

	void OnPlayerDead()
	{
		Assert.That(_state == GameStates.Playing);
		_state = GameStates.GameOver;
		_enemyManager.Stop();
	}
}
