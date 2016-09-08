using UnityEngine;
using Zenject;
using System;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
		InstallPlayer();
    }

	void InstallPlayer()
	{
		Container.Bind<PlayerStateFactory>().AsSingle();

		Container.BindFactory<PlayerStateRunning, PlayerStateRunning.Factory>();
		Container.BindFactory<PlayerStateJumping, PlayerStateJumping.Factory>();
	}
}