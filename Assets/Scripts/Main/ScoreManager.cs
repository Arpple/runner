using UnityEngine;
using System.Collections;
using Zenject;
using UnityEngine.UI;
using ModestTree;

public class ScoreManager
{
	Text _scoreText;
	Text _highScoreText;
	int _score = 0;

	[Inject]
	public void Construct(
		[Inject(Id = "ScoreText")]Text scoreText,
		[Inject(Id = "HighScoreText")]Text highScoreText
	)
	{
		_scoreText = scoreText;
		_highScoreText = highScoreText;
	}

	public void Initialize()
	{
		_score = 0;
		UpdateScoreText();
		UpdateHighScoreText();
	}

	public int LoadHighScore()
	{
		return PlayerPrefs.GetInt("HighScore", 0);
	}

	/// <summary>
	/// check new score
	/// </summary>
	/// <returns><c>true</c>, if score is new highscore, <c>false</c> otherwise.</returns>
	public bool NewScore()
	{
		int currentHighScore = LoadHighScore();
		if(_score> currentHighScore)
		{
			PlayerPrefs.SetInt("HighScore", _score);
			return true;
		}

		return false;
	}

	public void Tick(float playerSpeed)
	{
		_score += (int)playerSpeed;
		UpdateScoreText();
	}


	void UpdateScoreText()
	{
		Assert.That(_scoreText != null, "Score Text Object is not set");

		_scoreText.text = "Score : " + _score;
	}

	void UpdateHighScoreText()
	{
		Assert.That(_highScoreText != null, "HighScore Text Object is not set");

		_highScoreText.text = "Hi Score : " + LoadHighScore();
	}
		
}
