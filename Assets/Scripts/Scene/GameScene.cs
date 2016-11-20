using UnityEngine;
using System.Collections;
using ModestTree;
using System.Collections.Generic;

public class GameScene : MonoBehaviour
{
	public StaticLayer BackgroundObj;

	private List<MoveLayer> _moveLayerList;

	public void Initialize()
	{
		_moveLayerList = new List<MoveLayer>();
		foreach(Transform child in transform)
		{
			MoveLayer moveLayer = child.GetComponent<MoveLayer>();
			if(moveLayer != null)
			{
				_moveLayerList.Add(moveLayer);
			}
		}

		BackgroundObj.Initialize();
		_moveLayerList.ForEach(moveLayer => moveLayer.Initialize());
	}

	public void Tick(float playerSpeed)
	{
		BackgroundObj.Tick();
		_moveLayerList.ForEach(moveLayer => moveLayer.Tick(playerSpeed));
	}

	public void Stop()
	{
		BackgroundObj.Stop();
		_moveLayerList.ForEach(moveLayer => moveLayer.Stop());
	}
}
