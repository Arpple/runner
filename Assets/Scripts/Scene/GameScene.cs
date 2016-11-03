using UnityEngine;
using System.Collections;
using ModestTree;

public class GameScene : MonoBehaviour
{
	public GameObject Background;
	public IStaticScene _background;

	public GameObject Foreground;
	public IMovableScene _foreground;

	public void Initialize()
	{
		Assert.That(Background != null && Foreground != null, "BG-FG object not set");

		//get interface
		_background = Background.GetComponent<IStaticScene>();
		_foreground = Foreground.GetComponent<IMovableScene>();

		_background.Initialize();
		_foreground.Initialize();
	}

	public void Tick(float playerSpeed)
	{
		_background.Tick();
		_foreground.Tick(playerSpeed);
	}

	public void Stop()
	{
		_background.Stop();
		_foreground.Stop();
	}
}
