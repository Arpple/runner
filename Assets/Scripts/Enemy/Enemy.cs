using UnityEngine;
using System.Collections;

public enum EnemyTypes
{
	Cactus,
}

public abstract class Enemy : MonoBehaviour
{
	public abstract void Tick(float playerSpeed);
	public abstract void Dispose();
}
