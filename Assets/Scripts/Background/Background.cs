using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using System.Linq;
using ModestTree;

public class Background : MonoBehaviour 
{
	//Property
	List<SpriteRenderer> _childList;
	List<Vector3> _childOriginalPosition;

	[Inject]
	public void Construct(
	)
	{
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


	void GetChild()
	{
		_childList = new List<SpriteRenderer>();
		_childOriginalPosition = new List<Vector3>();

		//1
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			SpriteRenderer r = child.GetComponent<SpriteRenderer>();

			if (r != null)
			{
				_childList.Add(r);
			}
		}

		//just for fun lol
		/*Enumerable.Range(0, transform.childCount).ToList().ForEach( i => 
		{
				Transform child = transform.GetChild(i);
				SpriteRenderer r = child.GetComponent<SpriteRenderer>();

				if(r != null)
				{
					_childList.Add(r);
				}
		});*/

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
