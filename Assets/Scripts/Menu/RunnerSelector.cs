using UnityEngine;
using System.Collections;
using Zenject;
using System.Collections.Generic;
using UnityEngine.UI;

public class RunnerSelector
{
	public List<GameObject> List;
	public GameObject Selected;
	public Text Text;

	public Transform Main;
	public Transform ListContainer;

	public RunnerSelector(
		Transform menu
	)
	{
		Main = menu.Find("Runner");
		ListContainer = Main.Find("List");
		Text = Main.Find("Text").GetComponent<Text>();
	}

	public void Initialize(GameDatabase dataBase)
	{
		List = new List<GameObject>();

		dataBase.RunnerList.ForEach( 
			runnerObject => 
			{
				GameObject selectorObject = (GameObject)GameObject.Instantiate(dataBase.Selector);
				selectorObject.name = runnerObject.name;
				selectorObject.GetComponentInChildren<Text>().text = runnerObject.name;
				selectorObject.transform.SetParent(ListContainer, false);
				Selector selector = selectorObject.GetComponent<Selector>();
				selector.OnSelected = () => SetRunner(runnerObject);
				List.Add(selectorObject);
			});

		Cycler cycler = Main.GetComponentInChildren<Cycler>();
		cycler.SetCyclingList(List);
	}

	void SetRunner(GameObject runner)
	{
		Selected = runner;
		Text.text = "Runner Selected : \n" + runner.name;
		Debug.Log("Runner Selected : " + runner);
	}
}
