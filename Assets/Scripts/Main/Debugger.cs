using UnityEngine;
using System.Collections;
using Zenject;
using UnityEngine.UI;

public class Debugger : MonoBehaviour 
{
	#if DEBUG
	public GameObject PlayerSpeedText;

	Player _player;


	[Inject]
	public void Construct(
		Player player
	)
	{
		_player = player;
	}


	void Update()
	{
		UpdatePlayerSpeed();
	}


	void UpdatePlayerSpeed()
	{
		Text speedText = PlayerSpeedText.GetComponent<Text>();
		speedText.text = "speed = " + _player.currentSpeed.ToString();
	}
	#endif
}
