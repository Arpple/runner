using UnityEngine;
using Zenject;
using System;
using ModestTree;

public enum CoreObject
{
	Player,
}

public class GameInstaller : MonoInstaller
{
	[SerializeField]
	Settings _settings = null;

	[InjectOptional(Id = "GameInstaller_Runner")] GameObject _runner;
	[InjectOptional(Id = "GameInstaller_Equipment")] GameObject _equipment;

    public override void InstallBindings()
    {
		InstallMain();
		InstallRunner();
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

	void InstallRunner()
	{
		Container.BindSignal<Signals.PlayerDead>();
		Container.BindTrigger<Signals.PlayerDead.Trigger>();
		if(_runner != null)
		{
			Debug.Log("Runner Loaded : " + _runner);
			Container.Bind<IRunner>().FromPrefab(_runner).AsSingle().WhenNotInjectedInto<GameInstaller>();
		}
		else
		{
			Debug.Log("Runner Not Loaded : default loaded ");
			Assert.That(_settings.DefaultRunner!= null, "Default Runner is not set");
			Container.Bind<IRunner>().FromPrefab(_settings.DefaultRunner).AsSingle().WhenNotInjectedInto<GameInstaller>();
		}
	}

	void InstallWeapon()
	{
		if(_equipment != null)
		{
			Debug.Log("Equipment Loaded : " + _equipment);
			Container.Bind<IEquipment>().FromPrefab(_equipment).AsSingle().WhenNotInjectedInto<GameInstaller>();
		}
		else
		{
			Debug.Log("Equipment Not Loaded : default loaded ");
			Assert.That(_settings.DefaultEquipment!= null, "Default Equipment is not set");
			Container.Bind<IEquipment>().FromPrefab(_settings.DefaultEquipment).AsSingle().WhenNotInjectedInto<GameInstaller>();
		}
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
		Container.Bind<ScoreManager>().AsSingle();
		Container.Bind<LevelHelper>().AsSingle();
		Container.Bind<Debugger>().AsSingle();
	}

	[Serializable]
	public class Settings
	{
		public GameObject DefaultRunner;
		public GameObject DefaultEquipment;

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