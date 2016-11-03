using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using System.Linq;

public class MoveLayer : MonoBehaviour, IMovableScene
{
	List<Transform> _childList;
	List<Vector3> _childOriginalPositionList;

	LevelHelper _level;

	[Inject]
	public void Construct(
		LevelHelper level
	)
	{
		_level = level;

		_childList = new List<Transform>();
		_childOriginalPositionList = new List<Vector3>();

		//register child
		foreach(Transform child in transform)
		{
			_childList.Add(child);
		}

		//dupplicate for looping
		Transform original = _childList.FirstOrDefault();
		Transform dupplicate = Instantiate(original);
		dupplicate.SetParent(original.parent, false);
		dupplicate.MoveToRight(original);
		_childList.Add(dupplicate);

		_childOriginalPositionList = _childList.Select( child => child.transform.position).ToList();
	}

	public void Initialize()
	{			
		//reset position
		_childList.ZipDo(_childOriginalPositionList, 
			(child, childOriginalPosition) => 
			{
				child.transform.position = childOriginalPosition;
			}
		);
	}


	public void Tick(float playerSpeed)
	{
		//move
		transform.Translate(new Vector3(- playerSpeed * Time.deltaTime, 0, 0));

		//looping background
		Transform left = _childList.FirstOrDefault();
		SpriteRenderer leftSprite = left.GetComponent<SpriteRenderer>();

		if(leftSprite.bounds.max.x < _level.Left)
		{
			Transform right = _childList.LastOrDefault();
			left.MoveToRight(right);

			_childList.Remove(left);
			_childList.Add(left);
		}
	}

	public void Stop()
	{
		
	}
}
