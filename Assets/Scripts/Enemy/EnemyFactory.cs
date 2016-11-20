using UnityEngine;
using System.Collections;
using ModestTree;

public class EnemyFactory 
{
	readonly Cactus.Factory _cactusFactory;
	readonly Bird.Factory _birdFactory;
	readonly PinkCactus.Factory _pinkCactusFactory;
    readonly Ghosty.Factory _ghostyFactory;

	public EnemyFactory(
		Cactus.Factory cactusFactory,
		Bird.Factory birdFactory,
		PinkCactus.Factory pinkCactusFactory,
        Ghosty.Factory ghostyFactory
	)
	{
		_cactusFactory = cactusFactory;
		_birdFactory = birdFactory;
		_pinkCactusFactory = pinkCactusFactory;
        _ghostyFactory = ghostyFactory;
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
            case EnemyTypes.Ghosty:
                return (Enemy)_ghostyFactory.Create();
		}

		throw Assert.CreateException();
	}
}
