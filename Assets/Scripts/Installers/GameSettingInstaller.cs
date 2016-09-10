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

	[Serializable]
	public class PlayerSetting
	{
		public Player.Settings Player;
		public PlayerStateJumping.Settings JumpingSetting;
	}
		

    public override void InstallBindings()
    {
		Container.BindInstance(Player.Player);
		Container.BindInstance(Player.JumpingSetting);
    }
}