using UnityEngine;
using System.Collections;
using System;
using Zenject;
using ModestTree;

public class Ghosty : Enemy
{
	public float FlySpeed;

	public override void Initialize()
	{
        this.FlySpeed = UnityEngine.Random.Range(-FlySpeed, FlySpeed);
	}

	public override void Tick(float playerSpeed)
	{
		transform.Translate(new Vector3(-(playerSpeed+FlySpeed > 0 ? playerSpeed + FlySpeed : playerSpeed) * Time.deltaTime,
            (Mathf.PingPong(Time.time,3.0f)-1.5f)*Time.deltaTime, 
            0));
	}

	public override void Dispose()
	{
		GameObject.Destroy(gameObject);
	}

	public class Factory : Factory<Ghosty>
	{
	}


}
