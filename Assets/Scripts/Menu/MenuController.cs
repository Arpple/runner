using UnityEngine;
using System.Collections;
using Zenject;
using ModestTree;
using System.Collections.Generic;
using UnityEngine.UI;

public class MenuController : MonoBehaviour, IInitializable
{
	private ZenjectSceneLoader _sceneLoader;
	private GameDatabase _dataBase;
	private RunnerSelector _runner;
	private EquipmentSelector _equipment;
	private LevelSelector _level;


	[Inject]
	public void Constuct(
		ZenjectSceneLoader sceneLoader, 
		GameDatabase database
	)
	{
		_sceneLoader = sceneLoader;
		_dataBase = database;
	}


	public void Initialize()
	{
		Debug.Log("Menu Initializing");

		Transform customizeHolder = transform.Find("Customize");

		//Create Runner
		Assert.That(_dataBase.RunnerList.Count > 0, "Runner database not set");
		_runner = new RunnerSelector(customizeHolder);
		_runner.Initialize(_dataBase);

		//Create Equipment
		Assert.That(_dataBase.EquipmentList.Count > 0, "Equipment database not set");
		_equipment = new EquipmentSelector(customizeHolder);
		_equipment.Initialize(_dataBase);

		//Create Level
		Assert.That(_dataBase.LevelList.Count > 0, "Level database not set");
		_level = new LevelSelector(customizeHolder);
		_level.Initialize(_dataBase);
	}


	public void StartGame()
	{
		if(_runner.Selected == null
			|| _level.Selected == null
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
				container.Bind<GameObject>().WithId("GameInstaller_Level").FromInstance(_level.Selected).WhenInjectedInto<GameInstaller>();
			});
	}
}

