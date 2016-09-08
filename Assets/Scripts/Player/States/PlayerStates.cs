using UnityEngine;
using System.Collections;

public enum PlayerStates
{
	WaitForStart,
	Running,
	Jumping,
	Dead,
}

public abstract class PlayerState
{
	public abstract void Update();

	public virtual void Start()
	{}

	public virtual void Stop()
	{}
}