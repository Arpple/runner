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
	}

	public void Initialize()
	{
		if(_childList == null)
		{
			GetChild();
		}
			
		//reset position
		_childList.ZipDo(_childOriginalPositionList, 
			(child, childOriginalPosition) => 
			{
				child.transform.position = childOriginalPosition;
			}
		);
	}

	void GetChild()
	{
		_childList = new List<Transform>();
		_childOriginalPositionList = new List<Vector3>();

		//Get Child
		foreach(Transform child in transform)
		{
			_childList.Add(child);
		}

		_childList = _childList.OrderBy( child => child.transform.position.x).ToList();

		//remove gap
		Transform leftObj = _childList.FirstOrDefault();

		foreach(Transform child in _childList.Skip(1))
		{
			SpriteRenderer leftObjSprite = leftObj.GetComponent<SpriteRenderer>();
			Vector3 leftObjPosition = leftObj.position;
			Vector3 leftObjSize = leftObjSprite.bounds.max - leftObjSprite.bounds.min;
			child.position = new Vector3(leftObjPosition.x + leftObjSize.x,leftObjPosition.y, leftObjPosition.z);

			leftObj = child;
		}
		_childOriginalPositionList = _childList.Select( child => child.transform.position).ToList();
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
			SpriteRenderer rightSprite = right.GetComponent<SpriteRenderer>();

			Vector3 rightPosition = right.position;
			Vector3 rightSize = rightSprite.bounds.max - rightSprite.bounds.min;

			left.position = new Vector3(rightPosition.x + rightSize.x, left.position.y, left.position.z);

			_childList.Remove(left);
			_childList.Add(left);
		}
	}

	public void Stop()
	{
		
	}
}
