using UnityEngine;
using System.Collections;
using Zenject;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelSelector
{
	public List<GameObject> List;
	public GameObject Selected;
	public Text Text;

	public Transform Main;
	public Transform ListContainer;

	public LevelSelector(
		Transform customizeMenu
	)
	{
		Main = customizeMenu.Find("Level");
		ListContainer = Main.Find("List");
		Text = Main.Find("Text").GetComponent<Text>();
	}

	public void Initialize(GameDatabase dataBase)
	{
		SetText("--");
		List = new List<GameObject>();

		dataBase.LevelList.ForEach( 
			levelObject => 
			{
				GameObject selectorObject = (GameObject)GameObject.Instantiate(dataBase.Selector);
				string name;
				Selector selector = selectorObject.GetComponent<Selector>();
				selector.OnSelected = () => SetLevel(levelObject);
				if(levelObject != null)
				{
					name = levelObject.name;
				}
				else
				{
					name = "None";
				}
				selectorObject.name = name;
				selectorObject.GetComponentInChildren<Text>().text = name;
				selectorObject.transform.SetParent(ListContainer, false);
				List.Add(selectorObject);
			});

		Cycler cycler = Main.GetComponentInChildren<Cycler>();
		cycler.SetCyclingList(List);
	}

	void SetLevel(GameObject level)
	{
		Selected = level;
		string name = "None";
		if(level != null)
			name = level.name;
		SetText(name);
		Debug.Log("Level Selected : " + name);
	}

	void SetText(string name)
	{
		Text.text = "-- Level Selected --\n" + name;
	}
}
