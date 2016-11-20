using UnityEngine;
using System.Collections.Generic;

public class EquipmentSlot : MonoBehaviour 
{
	public List<GameObject> Slots;

	public bool SetEquipment(GameObject equipmentObj, string slotName)
	{
		var slot = Slots.Find(s => s.name == slotName);
		if(slot != null)
		{
			equipmentObj.transform.SetParent(slot.transform, false);
			return true;
		}
		return false;
	}
}
