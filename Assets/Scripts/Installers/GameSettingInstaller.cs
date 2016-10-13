using UnityEngine;
using Zenject;
using System;
using UnityEditor;

public class GameSettingInstaller : ScriptableObjectInstaller
{
	[MenuItem("Assets/Create/ScriptableObject/GameSetting")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<GameSettingInstaller> ();
	}


	public RunnerSettings Runner;
	public EquipmentSetting Equipment;
	public EnemySeting Enemy;


	public override void InstallBindings()
	{
		Container.BindInstance(Runner.Runner);

		Container.BindInstance(Equipment.Cannon);
		Container.BindInstance(Equipment.CannonBullet);

		Container.BindInstance(Enemy.ManagerSetting);
	}


	[Serializable]
	public class RunnerSettings
	{
		public IRunnerSettings Runner;
	}

	[Serializable]
	public class EquipmentSetting
	{
		public Cannon.Settings Cannon;
		public CannonBullet.Settings CannonBullet;
	}

		
	[Serializable]
	public class EnemySeting
	{
		public EnemyManager.Settings ManagerSetting;
	}

   
}