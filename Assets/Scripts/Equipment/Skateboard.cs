using UnityEngine;
using System.Collections;
using System;
using Zenject;

public class Skateboard : MonoBehaviour, IEquipment
{
	private Settings _settings;
	private IRunner _runner;

	[Inject]
	public void Construct(
		Settings settings,
		IRunner runner
	)
	{
		_settings = settings;
		_runner = runner;
		var runnerSlot = _runner.GetEquipmentSlot();
		if(runnerSlot != null)
		{
			runnerSlot.SetEquipment(gameObject, "Foot");
		}
	}

	public void Initialize()
	{
		Activate();
	}

	public void Tick(){}
	public void Activate()
	{
		_runner.CurrentSpeed += _settings.AdditionalSpeed;
	}

	[Serializable]
	public class Settings
	{
		public int AdditionalSpeed;
	}
}


