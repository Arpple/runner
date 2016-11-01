using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameDatabase : ScriptableObject 
{
	#if UNITY_EDITOR
	[MenuItem("Assets/Create/ScriptableObject/GameDatabase")]
	public static void CreateAsset ()
	{
		ScriptableObjectUtility.CreateAsset<GameDatabase> ();
	}
	#endif

	public GameObject Selector;
	public List<GameObject> RunnerList;
	public List<GameObject> EquipmentList;
}
