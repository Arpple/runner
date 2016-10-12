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
		InstallWeapon();
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
		Container.BindSignal<Signals.PlayerDead>();
		Container.BindTrigger<Signals.PlayerDead.Trigger>();
	}

	void InstallWeapon()
	{
		Container.BindFactory<IBullet, BulletFactory>().To<CannonBullet>().FromPrefab(_settings.WeaponSettings.CannonBulletPrefab);
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
		public Weapon WeaponSettings;

		[Serializable]
		public class Enemy
		{
			public GameObject CactusPrefab;
		}

		[Serializable]
		public class Weapon
		{
			public GameObject CannonBulletPrefab;
		}
	}
}