using UnityEngine;
using System.Collections;
using ModestTree;
using System.Collections.Generic;

public class GameScene : MonoBehaviour
{
	public StaticLayer BackgroundObj;
	public List<MoveLayer> MoveLayerList;

	public void Initialize()
	{
		BackgroundObj.Initialize();
		MoveLayerList.ForEach(moveLayer => moveLayer.Initialize());
	}

	public void Tick(float playerSpeed)
	{
		BackgroundObj.Tick();
		MoveLayerList.ForEach(moveLayer => moveLayer.Tick(playerSpeed));
	}

	public void Stop()
	{
		BackgroundObj.Stop();
		MoveLayerList.ForEach(moveLayer => moveLayer.Stop());
	}
}
