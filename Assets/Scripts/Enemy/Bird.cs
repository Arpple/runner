using UnityEngine;
using System.Collections;
using System;
using Zenject;
using ModestTree;

public class Bird : Enemy
{
	public float FlySpeed;

	public override void Initialize()
	{
	}

	public override void Tick(float playerSpeed)
	{
		transform.Translate(new Vector3(- (playerSpeed + FlySpeed) * Time.deltaTime, 0 , 0));
	}

	public override void Dispose()
	{
		GameObject.Destroy(gameObject);
	}

	public class Factory : Factory<Bird>
	{
	}
}
