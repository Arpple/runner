using UnityEngine;
using System.Collections;
using Zenject;
using ModestTree;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuController : MonoBehaviour, IInitializable
{
	ZenjectSceneLoader _sceneLoader;
	GameDatabase _dataBase;

	[Inject]
	public void Constuct(
		ZenjectSceneLoader sceneLoader, 
		GameDatabase database
	)
	{
		_sceneLoader = sceneLoader;
		_dataBase = database;
	}

	RunnerSelector _runner;
	EquipmentSelector _equipment;

	public void Initialize()
	{
		Debug.Log("Menu Initializing");

		//Create Runner
		Assert.That(_dataBase.RunnerList.Count > 0, "Runner database not set");
		_runner = new RunnerSelector(transform);
		_runner.Initialize(_dataBase);

		//Create Equipment
		Assert.That(_dataBase.EquipmentList.Count > 0, "Equipment database not set");
		_equipment = new EquipmentSelector(transform);
		_equipment.Initialize(_dataBase);
	}


	public void StartGame()
	{
		if(_runner.Selected == null
		)
		{
			return;
		}
		
		_sceneLoader.LoadScene("main", 
			(container) => 
			{
				container.Bind<GameObject>().WithId("GameInstaller_Runner").FromInstance(_runner.Selected).AsSingle().WhenInjectedInto<GameInstaller>();
				if(_equipment.Selected != null)
					container.Bind<GameObject>().WithId("GameInstaller_Equipment").FromInstance(_equipment.Selected).WhenInjectedInto<GameInstaller>();
			});
	}
}

