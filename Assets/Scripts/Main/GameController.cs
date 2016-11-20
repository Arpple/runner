using UnityEngine;
using System;
using System.Collections;
using Zenject;
using ModestTree;
using UnityEngine.SceneManagement;

public class GameController : IInitializable, ITickable, IDisposable
{

	//Property
	private GameStates _state = GameStates.WaitingToStart;

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
	readonly ScoreManager _score;
	readonly GameScene _scene;
	readonly GameOverUI _gameOverUI;

	public GameController(
		IRunner runner,
		EnemyManager enemyManager,
		GameScene scene,
		Signals.PlayerDead playerDeadSignal,
		ScoreManager score,
		GameOverUI gameOverUI
	)
	{
		_runner = runner;
		_enemyManager = enemyManager;
		_playerDeadSignal = playerDeadSignal;
		_score = score;
		_scene = scene;
		_gameOverUI = gameOverUI;
	}


	public void Initialize()
	{
		_playerDeadSignal.Event += OnPlayerDead;

		//Add GameOverUI  button action
		_gameOverUI.Hide();
		_gameOverUI.RetryButton.onClick.AddListener(StartGame);
		_gameOverUI.ExitButton.onClick.AddListener(Dispose);
	}


	public void Dispose()
	{
		_playerDeadSignal.Event -= OnPlayerDead;
		SceneManager.LoadScene("menu");
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
		_score.Tick(_runner.CurrentSpeed);
		_scene.Tick(_runner.CurrentSpeed);

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
//		if(Input.GetMouseButtonDown(0))
//		{
//			StartGame();
//		}
	}
	#endregion

	void StartGame()
	{
		Assert.That(_state == GameStates.WaitingToStart || _state == GameStates.GameOver);
		_state = GameStates.Playing;
		_runner.Initialize();
		_enemyManager.Initialize();
		_score.Initialize();
		_scene.Initialize();
		_gameOverUI.Hide();
	}


	void OnPlayerDead()
	{
		Assert.That(_state == GameStates.Playing);
		_state = GameStates.GameOver;
		_runner.Stop();
		_enemyManager.Stop();
		_score.NewScore();
		_scene.Stop();
		_gameOverUI.Show();
	}

}
