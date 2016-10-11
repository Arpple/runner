using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using System.Linq;
using ModestTree;

public class Background : MonoBehaviour 
{
	//Property
	List<Transform> _childList;
	List<Vector3> _childOriginalPosition;

	LevelHelper _level;

	[Inject]
	public void Construct(
		LevelHelper level
	)
	{
		_level = level;
	}


	public void Initialize()
	{
		GetChild();
		ResetPosition();
	}
		

	public void Tick(float playerSpeed)
	{
		Move(playerSpeed);
		LoopBackground();
	}

	public void Stop()
	{
	}


	void GetChild()
	{
		_childList = new List<Transform>();
		_childOriginalPosition = new List<Vector3>();

		foreach(Transform child in transform)
		{
			_childList.Add(child);
		}

		_childList = _childList.OrderBy( t => t.transform.position.x).ToList();
		_childOriginalPosition = _childList.Select( c => c.transform.position).ToList();
	}

	void ResetPosition()
	{
		for(int i = 0; i < _childList.Count; i++)
		{
			_childList[i].transform.position = _childOriginalPosition[i];
		}
	}


	void Move(float playerSpeed)
	{
		transform.Translate(new Vector3(- playerSpeed * Time.deltaTime, 0, 0));
	}


	void LoopBackground()
	{
		Transform left = _childList.FirstOrDefault();
		SpriteRenderer leftSprite = left.GetComponent<SpriteRenderer>();

		if(leftSprite.bounds.max.x < _level.Left)
		{
			Transform right = _childList.LastOrDefault();
			SpriteRenderer rightSprite = right.GetComponent<SpriteRenderer>();

			Vector3 rightPosition = right.position;
			Vector3 rightSize = rightSprite.bounds.max - rightSprite.bounds.min;

			left.position = new Vector3(rightPosition.x + rightSize.x, left.position.y, left.position.z);

			_childList.Remove(left);
			_childList.Add(left);
		}
	}
}
