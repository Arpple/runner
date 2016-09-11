using UnityEngine;
using System.Collections;
using ModestTree;

public class EnemyFactory 
{
	readonly Cactus.Factory _cactusFactory;

	public EnemyFactory(
		Cactus.Factory cactusFactory
	)
	{
		_cactusFactory = cactusFactory;
	}

	public Enemy CreateEnemy(EnemyTypes type)
	{
		switch(type)
		{
			case EnemyTypes.Cactus:
				return (Enemy)_cactusFactory.Create();
		}

		throw Assert.CreateException();
	}
}
