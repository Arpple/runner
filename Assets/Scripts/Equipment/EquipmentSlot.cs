using UnityEngine;
using System.Collections.Generic;

public class EquipmentSlot : MonoBehaviour 
{
	public List<GameObject> Slots;

	public bool SetEquipment(GameObject equipmentObj, string slotName)
	{
		foreach(var slot in Slots)
		{
			if(slot.name == slotName)
			{
				equipmentObj.transform.SetParent(slot.transform, false);
				return true;
			}
		}
		return false;
	}
}
