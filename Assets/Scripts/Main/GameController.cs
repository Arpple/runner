using UnityEngine;
using System;
using System.Collections;
using Zenject;
using ModestTree;

public class GameController : IInitializable, ITickable, IDisposable
{

	//Property
	GameStates _state = GameStates.WaitingToStart;
	[InjectOptional(Id = "runner_container")] GameObject RunnerContainer;

	public enum GameStates
	{
		WaitingToStart,
		Playing,
		GameOver,
	}

	//Dependency
	readonly IRunner _runner;
	readonly EnemyManager _enemyManager;
	readonly Signals.PlayerDead _playerDeadSignal;
	readonly Background _background;
	readonly ScoreManager _score;

	public GameController(
		IRunner runner,
		Background background,
		EnemyManager enemyManager,
		Signals.PlayerDead playerDeadSignal,
		ScoreManager score
	)
	{
		_runner = runner;
		_enemyManager = enemyManager;
		_playerDeadSignal = playerDeadSignal;
		_background = background;
		_score = score;
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
		_runner.Tick();
		_enemyManager.Tick(_runner.CurrentSpeed);
		_background.Tick(_runner.CurrentSpeed);
		_score.Tick(_runner.CurrentSpeed);

		PlayerInputUpdate();
	}

	void PlayerInputUpdate()
	{
		Assert.That(_state == GameStates.Playing);

		if(Input.GetMouseButtonDown(0))
		{
			_runner.StartAction();
		}

		if(Input.GetMouseButton(0))
		{
			_runner.HoldAction();
		}

		if(Input.GetMouseButtonUp(0))
		{
			_runner.StopAction();
		}
	}

	void UpdateGameOver()
	{
		Assert.That(_state == GameStates.GameOver);
		if(Input.GetMouseButtonDown(0))
		{
			StartGame();
		}
	}
	#endregion

	void StartGame()
	{
		Assert.That(_state == GameStates.WaitingToStart || _state == GameStates.GameOver);
		_state = GameStates.Playing;
		_runner.Initialize();
		_enemyManager.Initialize();
		_background.Initialize();
		_score.Initialize();
	}


	void OnPlayerDead()
	{
		Assert.That(_state == GameStates.Playing);
		_state = GameStates.GameOver;
		_runner.Stop();
		_enemyManager.Stop();
		_background.Stop();
		_score.NewScore();
	}

}
