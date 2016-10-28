using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
	public GameDatabase database;

    public override void InstallBindings()
    {
		Container.BindInstance(database).AsSingle();
    }
}