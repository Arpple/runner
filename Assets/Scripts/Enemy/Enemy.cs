using UnityEngine;
using System.Collections;

public enum EnemyTypes
{
	Cactus,
	Bird,
}

public abstract class Enemy : MonoBehaviour
{
	public abstract void Initialize();
	public abstract void Tick(float playerSpeed);
	public abstract void Dispose();
}
