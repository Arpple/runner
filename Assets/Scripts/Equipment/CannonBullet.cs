using UnityEngine;
using System.Collections;
using System;
using Zenject;
using ModestTree;

public class CannonBullet : MonoBehaviour, IBullet
{
	Settings _settings;
	float _lifeTimeCounter;

	[Inject]
	public void Construct(
		Settings settings
	)
	{
		_settings = settings;
	}

	public void Initialize(IEquipment weapon)
	{
		_lifeTimeCounter = _settings.LifeTime;
	}

	public void SetPosition(Vector3 position)
	{
		transform.position = position;
	}

	public void Tick()
	{
		Move();
		_lifeTimeCounter -= Time.deltaTime;
	}

	public bool CheckDispose()
	{
		return _lifeTimeCounter <= 0;
	}

	public void Dispose()
	{
		Destroy(gameObject);
	}

	void Move()
	{
		transform.Translate(new Vector3(_settings.Speed * Time.deltaTime, 0, 0));
	}

	[Serializable]
	public class Settings
	{
		public float Speed;
		public float LifeTime;
	}
}
