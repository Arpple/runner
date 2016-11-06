using UnityEngine;
using System.Collections;
using Zenject;
using System.Collections.Generic;
using UnityEngine.UI;

public class EquipmentSelector
{
	public List<GameObject> List;
	public GameObject Selected;
	public Text Text;

	public Transform Main;
	public Transform ListContainer;

	public EquipmentSelector(
		Transform customizeMenu
	)
	{
		Main = customizeMenu.Find("Equipment");
		ListContainer = Main.Find("List");
		Text = Main.Find("Text").GetComponent<Text>();
	}

	public void Initialize(GameDatabase dataBase)
	{
		SetText("--");
		List = new List<GameObject>();

		dataBase.EquipmentList.ForEach( 
			equipmentObject => 
			{
				GameObject selectorObject = (GameObject)GameObject.Instantiate(dataBase.Selector);
				string name;
				Selector selector = selectorObject.GetComponent<Selector>();
				selector.OnSelected = () => SetEquipment(equipmentObject);
				if(equipmentObject != null)
				{
					name = equipmentObject.name;
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

	void SetEquipment(GameObject equipment)
	{
		Selected = equipment;
		string name = "None";
		if(equipment != null)
			name = equipment.name;
		SetText(name);
		Debug.Log("Equipment Selected : " + name);
	}

	void SetText(string name)
	{
		Text.text = "-- Equipment Selected --\n" + name;
	}
}
