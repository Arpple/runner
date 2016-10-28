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
	public void Constuct(ZenjectSceneLoader sceneLoader, 
		GameDatabase database
	)
	{
		_sceneLoader = sceneLoader;
		_dataBase = database;
	}

	List<GameObject> _runnerList;
	GameObject _selectedRunner;
	Text _selectedRunnerText;

	public void SetRunner(GameObject runner)
	{
		_selectedRunner = runner;
		_selectedRunnerText.text = "Runner Selected : \n" + runner.name;
		Debug.Log("Runner Selected : " + _selectedRunner);
	}

	public void Initialize()
	{
		Debug.Log("Menu Initializing");
		CreateRunnerSelectors();
	}

	public void CreateRunnerSelectors()
	{
		Assert.That(_dataBase != null);

		_runnerList = new List<GameObject>();
		Transform runnerMenu = this.transform.FindChild("Runner");

		if(runnerMenu == null)
		{
			runnerMenu = (new GameObject("Runner")).transform;
			runnerMenu.SetParent(this.transform, false);
		}

		Transform runnerContainer = runnerMenu.FindChild("List");
			
		_dataBase.RunnerList.ForEach( 
			runnerObject => 
			{
				GameObject selectorObject = (GameObject)Instantiate(_dataBase.Selector);
				selectorObject.name = runnerObject.name;
				selectorObject.GetComponentInChildren<Text>().text = runnerObject.name;
				selectorObject.transform.SetParent(runnerContainer, false);
				Selector selector = selectorObject.GetComponent<Selector>();
				selector.OnSelected = () => SetRunner(runnerObject);
				_runnerList.Add(selectorObject);
			});

		Cycler cycler = runnerMenu.GetComponentInChildren<Cycler>();
		cycler.SetCyclingList(_runnerList);

		_selectedRunnerText = runnerMenu.FindChild("Text").GetComponent<Text>();
	}

	public void StartGame()
	{
		if(_selectedRunner == null)
			return;
		
		_sceneLoader.LoadScene("main", 
			(container) => 
			{
				container.Bind<IRunner>().FromPrefab(_selectedRunner).AsSingle();
			});
	}
}

