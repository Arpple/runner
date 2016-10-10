using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using System.Linq;

public class Background : MonoBehaviour 
{
	//Property
	List<SpriteRenderer> _childList;
	List<Vector3> _childOriginalPosition;

	//Dependency
	Player _player;

	[Inject]
	public void Construct(
		Player player
	)
	{
		_player = player;
		GetChild();
	}


	public void Initialize()
	{
		for(int i = 0; i < _childList.Count; i++)
		{
			_childList[i].transform.position = _childOriginalPosition[i];
		}
	}
		

	public void Tick()
	{
		Move();
		LoopBackground();
	}


	void GetChild()
	{
		_childList = new List<SpriteRenderer>();
		_childOriginalPosition = new List<Vector3>();

		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			SpriteRenderer r = child.GetComponent<SpriteRenderer>();

			if (r != null)
			{
				_childList.Add(r);
			}
		}
		_childList = _childList.OrderBy( t => t.transform.position.x).ToList();
		_childOriginalPosition = _childList.Select( c => c.transform.position).ToList();
	}


	void Move()
	{
	}


	void LoopBackground()
	{
		SpriteRenderer mostLeft = _childList.FirstOrDefault();
		if(mostLeft != null)
		{
			if(mostLeft.transform.position.x < Camera.main.transform.position.x)
			{
				if(mostLeft.IsVisibleFrom(Camera.main) == false)
				{
					SpriteRenderer mostRight = _childList.LastOrDefault();
					Vector3 rightPosition = mostRight.transform.position;
					Vector3 rightSize = (mostRight.bounds.max - mostRight.bounds.min);

					mostLeft.transform.position = new Vector3(rightPosition.x + rightSize.x, mostLeft.transform.position.y, mostLeft.transform.position.z);

					_childList.Remove(mostLeft);
					_childList.Add(mostLeft);
				}
			}
		}
	}
}
