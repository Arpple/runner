using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;

public class GameDatabase : ScriptableObject 
{
	[MenuItem("Assets/Create/ScriptableObject/GameDatabase")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<GameDatabase> ();
	}

	public GameObject Selector;
	public List<GameObject> RunnerList;
}
