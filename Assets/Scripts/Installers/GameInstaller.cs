using UnityEngine;
using Zenject;
using System;

public class GameInstaller : MonoInstaller
{
	[SerializeField]
	Settings _settings = null;

    public override void InstallBindings()
    {
		InstallMain();
		InstallPlayer();
		InstallEnemy();
		InstallMisc();
    }

	void InstallMain()
	{
		Container.Bind<IInitializable>().To<GameController>().AsSingle();
		Container.Bind<ITickable>().To<GameController>().AsSingle();
		Container.Bind<IDisposable>().To<GameController>().AsSingle();
	}

	void InstallPlayer()
	{
		Container.Bind<PlayerStateFactory>().AsSingle();

		Container.BindFactory<PlayerStateRunning, PlayerStateRunning.Factory>();
		Container.BindFactory<PlayerStateJumping, PlayerStateJumping.Factory>();

		Container.BindSignal<Signals.PlayerDead>();
		Container.BindTrigger<Signals.PlayerDead.Trigger>();
	}

	void InstallEnemy()
	{
		Container.Bind<EnemyManager>().AsSingle();

		Container.Bind<EnemyFactory>().AsSingle();
		Container.BindFactory<Cactus, Cactus.Factory>().FromPrefab(_settings.EnemySettings.CactusPrefab);
	}

	void InstallMisc()
	{
		Container.Bind<LevelHelper>().AsSingle();
		Container.Bind<Debugger>().AsSingle();
	}

	[Serializable]
	public class Settings
	{
		public Enemy EnemySettings;

		[Serializable]
		public class Enemy
		{
			public GameObject CactusPrefab;
		}
	}
}