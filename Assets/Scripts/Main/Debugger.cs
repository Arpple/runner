using UnityEngine;
using System.Collections;
using Zenject;
using UnityEngine.UI;

public class Debugger : MonoBehaviour 
{
	#if DEBUG

	[Inject(Id = "debug_speed")] Text _speedText;
	[Inject(Id = "debug_jump")] Text _jumpText;

	IRunner _runner;
	public static Debugger instance;

	[Inject]
	public void Construct(
		IRunner runner
	)
	{
		_runner = runner;
		Debugger.instance = this;
	}


	void Update()
	{
		UpdatePlayerSpeed();
	}


	void UpdatePlayerSpeed()
	{
		_speedText.text = "speed = " + _runner.CurrentSpeed.ToString();
	}

	public void UpdatePlayerJump(float jumpPower)
	{
		_jumpText.text = "last jump = " + jumpPower.ToString();
	}
	#endif
}
