using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
		//Container.Bind<ZenjectSceneLoader>().AsSingle();
    }
}