using System;
using UnityEngine;

public class Selector : MonoBehaviour
{
	public Action OnSelected;

	public void Select()
	{
		OnSelected.Invoke();
	}
}
