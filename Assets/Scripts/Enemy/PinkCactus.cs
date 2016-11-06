using UnityEngine;
using System.Collections;
using System;
using Zenject;
using ModestTree;

public class PinkCactus : Enemy
{
	public override void Initialize()
	{
		
	}

	public override void Tick(float playerSpeed)
	{
		transform.Translate(new Vector3(- playerSpeed * Time.deltaTime, 0 , 0));
	}

	public override void Dispose()
	{
		GameObject.Destroy(gameObject);
	}

	public class Factory : Factory<PinkCactus>
	{
	}
}
