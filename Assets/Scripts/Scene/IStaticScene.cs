using System;
using UnityEngine;
using System.Collections;

public interface IStaticScene
{
	void Initialize();
	void Tick();
	void Stop();
}
