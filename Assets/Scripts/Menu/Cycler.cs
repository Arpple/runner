using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ModestTree;

public class Cycler : MonoBehaviour 
{
	private List<GameObject> _cyclingList;
	private int _index;

	public void SetCyclingList(List<GameObject> list)
	{
		Assert.That(list != null && list.Count > 0);
		_cyclingList = list;

		list.ForEach(obj => obj.SetActive(false));

		_index = 0;
		_cyclingList[_index].SetActive(true);
	}

	public void Next()
	{
		Assert.That(_cyclingList != null && _cyclingList.Count > 0);
	
		_cyclingList[_index].SetActive(false);

		// go to first if at last
		if(_index == _cyclingList.Count - 1)
		{
			_index = 0;
		}
		else
		{
			_index++;
		}
	
		_cyclingList[_index].SetActive(true);
	}
}