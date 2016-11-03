using UnityEngine;
using System.Collections;
using ModestTree;

public class GameScene : MonoBehaviour
{
	public StaticLayer BackgroundObj;
	public MoveLayer ForegroundObj;

	public void Initialize()
	{
		Assert.That(BackgroundObj != null && ForegroundObj != null, "BG-FG object not set");

		BackgroundObj.Initialize();
		ForegroundObj.Initialize();
	}

	public void Tick(float playerSpeed)
	{
		BackgroundObj.Tick();
		ForegroundObj.Tick(playerSpeed);
	}

	public void Stop()
	{
		BackgroundObj.Stop();
		ForegroundObj.Stop();
	}
}
