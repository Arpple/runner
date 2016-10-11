using UnityEngine;
using System.Collections;
using System;
using Zenject;
using ModestTree;

public class Cactus : Enemy
{

	[Inject]
	public void Construct(
	)
	{
	}

	public override void Tick(float playerSpeed)
	{
		Move(playerSpeed);
	}

	void Move(float playerSpeed)
	{
		transform.Translate(new Vector3(- playerSpeed * Time.deltaTime, 0 , 0));
	}

	public override void Dispose()
	{
		GameObject.Destroy(gameObject);
	}

	public class Factory : Factory<Cactus>
	{
	}
}
