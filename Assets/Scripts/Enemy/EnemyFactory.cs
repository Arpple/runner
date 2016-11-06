using UnityEngine;
using System.Collections;
using ModestTree;

public class EnemyFactory 
{
	readonly Cactus.Factory _cactusFactory;
	readonly Bird.Factory _birdFactory;

	public EnemyFactory(
		Cactus.Factory cactusFactory,
		Bird.Factory birdFactory
	)
	{
		_cactusFactory = cactusFactory;
		_birdFactory = birdFactory;
	}

	public Enemy CreateEnemy(EnemyTypes type)
	{
		switch(type)
		{
			case EnemyTypes.Cactus:
				return (Enemy)_cactusFactory.Create();
			case EnemyTypes.Bird:
				return (Enemy)_birdFactory.Create();
		}

		throw Assert.CreateException();
	}
}
