using UnityEngine;
using System.Collections;
using System;

public interface IRunner
{
	float CurrentSpeed { get; set; }

	void Initialize();
	void Tick();
	void Stop();

	void StartAction();
	void HoldAction();
	void StopAction();
}

[Serializable]
public class IRunnerSettings
{
	public float InitialSpeed;
	public float Acceleration;
	public float MaximumSpeed;

	public float Gravity;

	public float MinJumpForce;
	public float MinJumpHoldTime;
	public float MaxJumpForce;
	public float MaxJumpHoldTime;
}