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
	class RunnerSelector
	{
		public List<GameObject> List;
		public GameObject Selected;
		public Text Text;

		public Transform Main;
		public Transform ListContainer;

		public RunnerSelector(Transform menu)
		{
			Main = menu.Find("Runner");
			ListContainer = Main.Find("List");
			Text = Main.Find("Text").GetComponent<Text>();
		}
	}
		
	public void SetRunner(GameObject runner)
	{
		_runner.Selected = runner;
		_runner.Text.text = "Runner Selected : \n" + runner.name;
		Debug.Log("Runner Selected : " + runner);
	}

	public void Initialize()
	{
		Debug.Log("Menu Initializing");
		CreateRunnerSelectors();
	}

	public void CreateRunnerSelectors()
	{
		Assert.That(_dataBase != null);
		_runner = new RunnerSelector(transform);

		_runner.List = new List<GameObject>();
			
		_dataBase.RunnerList.ForEach( 
			runnerObject => 
			{
				GameObject selectorObject = (GameObject)Instantiate(_dataBase.Selector);
				selectorObject.name = runnerObject.name;
				selectorObject.GetComponentInChildren<Text>().text = runnerObject.name;
				selectorObject.transform.SetParent(_runner.ListContainer, false);
				Selector selector = selectorObject.GetComponent<Selector>();
				selector.OnSelected = () => SetRunner(runnerObject);
				_runner.List.Add(selectorObject);
			});

		Cycler cycler = _runner.Main.GetComponentInChildren<Cycler>();
		cycler.SetCyclingList(_runner.List);
	}

	public void StartGame()
	{
		if(_runner.Selected == null)
			return;
		
		_sceneLoader.LoadScene("main", 
			(container) => 
			{
				Debug.Log("Inject : " + _runner.Selected);
				container.Bind<GameObject>().WithId("GameInstaller_Runner").FromInstance(_runner.Selected).AsSingle().WhenInjectedInto<GameInstaller>();
			});
	}
}

