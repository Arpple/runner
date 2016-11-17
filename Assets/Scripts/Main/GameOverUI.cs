using UnityEngine;
using System.Collections;
using Zenject;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour 
{
	public Button RetryButton;
    public Button ExitButton;

	public void Show()
	{
		gameObject.SetActive(true);

	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
