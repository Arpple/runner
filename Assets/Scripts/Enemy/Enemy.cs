using UnityEngine;
using System.Collections;

public enum EnemyTypes
{
	Cactus,
	Bird,
	PinkCactus,
}

public abstract class Enemy : MonoBehaviour
{
	protected bool _isDead = false;

	public abstract void Initialize();
	public abstract void Tick(float playerSpeed);
	public abstract void Dispose();

	public void Dead()
	{
		_isDead = true;
	}

	public bool IsDead()
	{
		return _isDead;
	}

	public void OnTriggerEnter2D(Collider2D otherCollider)
	{
		var trigger = otherCollider.GetComponent<IEnemyHitable>();
		if(trigger != null)
		{
			trigger.OnHitEnemy(this);
		}
	}
}
