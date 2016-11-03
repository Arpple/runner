using UnityEngine;
using System.Collections;

public interface IMovableScene
{
	void Initialize();
	void Tick(float playerSpeed);
	void Stop();
}
