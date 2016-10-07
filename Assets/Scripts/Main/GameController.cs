using UnityEngine;
using System;
using System.Collections;
using Zenject;
using ModestTree;

public class GameController : IInitializable, ITickable, IDisposable
{
	//Property
	GameStates _state = GameStates.WaitingToStart;

	public enum GameStates
	{
		WaitingToStart,
		Playing,
		GameOver,
	}

	//Dependency
	readonly Player _player;
	readonly EnemyManager _enemyManager;
	readonly Signals.PlayerDead _playerDeadSignal;
	readonly Background _background;

	public GameController(
		Player player,
		Background background,
		EnemyManager enemyManager,
		Signals.PlayerDead playerDeadSignal
	)
	{
		_player = player;
		_enemyManager = enemyManager;
		_playerDeadSignal = playerDeadSignal;
		_background = background;
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

	#region StateUpdate
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
		_background.Tick();

		PlayerInputUpdate();
	}

	void PlayerInputUpdate()
	{
		Assert.That(_state == GameStates.Playing);
		if(Input.GetMouseButton(0))
		{
			_player.Jump();
		}
	}

	void UpdateGameOver()
	{
		Assert.That(_state == GameStates.GameOver);
		if(Input.GetMouseButton(0))
		{
			StartGame();
		}
	}
	#endregion

	void StartGame()
	{
		Assert.That(_state == GameStates.WaitingToStart || _state == GameStates.GameOver);
		_state = GameStates.Playing;
		_player.Initialize();
		_enemyManager.Initialize();
		_background.Initialize();
	}


	void OnPlayerDead()
	{
		Assert.That(_state == GameStates.Playing);
		_state = GameStates.GameOver;
		_enemyManager.Stop();
	}

}
