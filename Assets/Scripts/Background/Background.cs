﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Zenject;
using System.Linq;

public class Background : MonoBehaviour 
{
	Player.Settings _playerSettings;

	[Inject]
	public void Construct(
		Player.Settings playerSettings
	)
	{
		_playerSettings = playerSettings;
	}

	private List<SpriteRenderer> _childList;

	void Start()
	{
		GetChild();
	}

	void Update()
	{
		Move(_playerSettings.MoveSpeed);
	}

	void GetChild()
	{
		_childList = new List<SpriteRenderer>();

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
	}

	public void Move(float speed)
	{
		transform.Translate(new Vector3(-speed, 0, 0));
		LoopBackground();
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