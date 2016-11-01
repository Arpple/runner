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
		Transform menu
	)
	{
		Main = menu.Find("Equipment");
		ListContainer = Main.Find("List");
		Text = Main.Find("Text").GetComponent<Text>();
	}

	public void Initialize(GameDatabase dataBase)
	{
		List = new List<GameObject>();

		dataBase.EquipmentList.ForEach( 
			equipmentObject => 
			{
				GameObject selectorObject = (GameObject)GameObject.Instantiate(dataBase.Selector);
				selectorObject.name = equipmentObject.name;
				selectorObject.GetComponentInChildren<Text>().text = equipmentObject.name;
				selectorObject.transform.SetParent(ListContainer, false);
				Selector selector = selectorObject.GetComponent<Selector>();
				selector.OnSelected = () => SetEquipment(equipmentObject);
				List.Add(selectorObject);
			});

		Cycler cycler = Main.GetComponentInChildren<Cycler>();
		cycler.SetCyclingList(List);
	}

	void SetEquipment(GameObject equipment)
	{
		Selected = equipment;
		Text.text = "Equipment Selected : \n" + equipment.name;
		Debug.Log("Equipment Selected : " + equipment);
	}
}
