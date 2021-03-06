﻿using UnityEngine;
using System.Collections;
using System;
using Zenject;
using System.Collections.Generic;
using System.Linq;
using ModestTree;

public class Cannon : MonoBehaviour, IEquipment
{
	private float _coolDownCounter;
	private Settings _settings;
	private IFactory<IBullet> _bulletFactory;
	private List<IBullet> _bullets;

	[Inject]
	public void Construct(
		Settings settings,
		BulletFactory bulletFactory,
		IRunner runner
	)
	{
		_settings = settings;
		_bulletFactory = bulletFactory;

		var runnerSlot = runner.GetEquipmentSlot();
		if(runnerSlot != null)
		{
			runnerSlot.SetEquipment(gameObject, "Hand");
		}
		else
		{
			transform.SetParent(runner.GetObject().transform, false);
		}
	}

	public void Initialize()
	{
		AddCooldown();
		if(_bullets != null)
		{
			_bullets.ForEach(b => b.Dispose());
		}
		_bullets = new List<IBullet>();

	}

	public void Tick()
	{
		Assert.That(_bullets != null, "Bullet is not set");

		_coolDownCounter -= Time.deltaTime;
		if(_coolDownCounter <= 0)
		{
			Activate();
			AddCooldown();
		}
		_bullets.RemoveAll(b => b == null);
		_bullets.ForEach(b => {
			b.Tick();
			if(b.CheckDispose())
			{
				b.Dispose();
				_bullets.Remove(b);
			}
		});

	}

	public void Activate()
	{
		Assert.That(_bullets != null, "Bullet is not set");

		IBullet bullet = _bulletFactory.Create();
		bullet.Initialize(this);
		bullet.SetPosition(new Vector2(transform.position.x+1f,transform.position.y));
		_bullets.Add(bullet);

	}

	public Vector3 GetSpawnPosition()
	{
		return transform.position;
	}

	void AddCooldown()
	{
		_coolDownCounter = _settings.CoolDown;
	}

	[Serializable]
	public class Settings
	{
		public float CoolDown;
	}
}
