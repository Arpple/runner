using UnityEngine;
using System.Collections;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public interface IRunner
{
	float CurrentSpeed { get; set; }

	void Initialize();
	void Tick();
	void Stop();

	void StartAction();
	void HoldAction();
	void StopAction();

	GameObject GetObject();
}


[Serializable]
public class IRunnerSettings : ScriptableObject
{
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/ScriptableObject/IRunnerSetting")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<IRunnerSettings>();
	}
	#endif

	public float InitialSpeed;
	public float Acceleration;
	public float MaximumSpeed;

	public float Gravity;

	public float MinJumpForce;
	public float MinJumpHoldTime;
	public float MaxJumpForce;
	public float MaxJumpHoldTime;
}