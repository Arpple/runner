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


	public PlayerSetting Player;
	public EnemySeting Enemy;


	public override void InstallBindings()
	{
		Container.BindInstance(Player.Player);

		Container.BindInstance(Enemy.ManagerSetting);
	}


	[Serializable]
	public class PlayerSetting
	{
		public Player.Settings Player;
	}

		
	[Serializable]
	public class EnemySeting
	{
		public EnemyManager.Settings ManagerSetting;
	}

   
}