using UnityEngine;
using Zenject;
using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameSettingInstaller : ScriptableObjectInstaller
{
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/ScriptableObject/GameSetting")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<GameSettingInstaller> ();
	}
	#endif

	public EquipmentSetting Equipment;
	public EnemySeting Enemy;

	public override void InstallBindings()
	{
		Container.BindInstance(Equipment.Cannon);
		Container.BindInstance(Equipment.CannonBullet);
		Container.BindInstance(Equipment.Skateboard);

		Container.BindInstance(Enemy.ManagerSetting);
	}

	[Serializable]
	public class EquipmentSetting
	{
		public Cannon.Settings Cannon;
		public CannonBullet.Settings CannonBullet;

		public Skateboard.Settings Skateboard;
	}

		
	[Serializable]
	public class EnemySeting
	{
		public EnemyManager.Settings ManagerSetting;
	}

   
}