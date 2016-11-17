using UnityEngine;
using System.Collections;
using Zenject;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
	public Button RetryButton;
    public Button ExitButton;
	public Text ScoreText;

	private ScoreManager _score;

	[Inject]
	public void Construct(
		ScoreManager scoreMan
	)
	{
		_score = scoreMan;
	}

	public void Show()
	{
		gameObject.SetActive(true);
		ScoreText.text = _score.GetScore().ToString();
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
