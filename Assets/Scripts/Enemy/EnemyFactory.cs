using UnityEngine;
using System.Collections;
using ModestTree;

public class EnemyFactory 
{
	readonly Cactus.Factory _cactusFactory;
	readonly Bird.Factory _birdFactory;
	readonly PinkCactus.Factory _pinkCactusFactory;

	public EnemyFactory(
		Cactus.Factory cactusFactory,
		Bird.Factory birdFactory,
		PinkCactus.Factory pinkCactusFactory
	)
	{
		_cactusFactory = cactusFactory;
		_birdFactory = birdFactory;
		_pinkCactusFactory = pinkCactusFactory;
	}

	public Enemy CreateEnemy(EnemyTypes type)
	{
		switch(type)
		{
			case EnemyTypes.Cactus:
				return (Enemy)_cactusFactory.Create();
			case EnemyTypes.Bird:
				return (Enemy)_birdFactory.Create();
			case EnemyTypes.PinkCactus:
				return (Enemy)_pinkCactusFactory.Create();
		}

		throw Assert.CreateException();
	}
}
