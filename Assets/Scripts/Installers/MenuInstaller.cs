using UnityEngine;
using Zenject;
using ModestTree;

public class MenuInstaller : MonoInstaller
{
	public GameDatabase database;

    public override void InstallBindings()
    {
		Assert.That(database != null, "Database is not set");

		Container.BindInstance(database).AsSingle();
    }
}