using UnityEngine;
using System.Collections;
using System;
using Zenject;
using ModestTree;

public class CannonBullet : MonoBehaviour, IBullet, IEnemyHitable
{
	private Settings _settings;
	private float _lifeTimeCounter;
	private bool _forceDispose = false;

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
		transform.Translate(new Vector3(_settings.Speed * Time.deltaTime, 0, 0));
		_lifeTimeCounter -= Time.deltaTime;
	}

	public bool CheckDispose()
	{
		return _lifeTimeCounter <= 0 || _forceDispose;
	}

	public void Dispose()
	{
		Destroy(gameObject);
	}

	public void OnHitEnemy(Enemy enemy)
	{
		enemy.Dead();
		_forceDispose = true;
	}

	[Serializable]
	public class Settings
	{
		public float Speed;
		public float LifeTime;
	}
}
