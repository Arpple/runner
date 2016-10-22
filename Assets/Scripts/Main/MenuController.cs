using UnityEngine;
using System.Collections;
using Zenject;
using ModestTree;

public class MenuController : MonoBehaviour
{
	ZenjectSceneLoader _sceneLoader;

	[Inject]
	public void Constuct(ZenjectSceneLoader sceneLoader)
	{
		_sceneLoader = sceneLoader;
	}

	GameObject _selectedRunner;

	public void SetRunner(GameObject runner)
	{
		_selectedRunner = runner;
		Debug.Log(_selectedRunner);
	}

	public void StartGame()
	{
		if(_selectedRunner == null)
			return;
		
		_sceneLoader.LoadScene("main", 
			(container) => 
			{
				container.Bind<IRunner>().FromPrefab(_selectedRunner).AsSingle();
			});
	}
}

