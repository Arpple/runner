using UnityEngine;
using System.Collections;
using Zenject;
using ModestTree;

public class PlayerStateFactory 
{
	readonly PlayerStateRunning.Factory _runningFactory;
	readonly PlayerStateJumping.Factory _jumpingFactory;

	public PlayerStateFactory(
		PlayerStateRunning.Factory runningFactory,
		PlayerStateJumping.Factory jumpingFactory
	)
	{
		_runningFactory = runningFactory;
		_jumpingFactory = jumpingFactory;
	}

	public PlayerState CreateState(PlayerStates state)
	{
		switch (state)
		{
			case PlayerStates.Running:
				return _runningFactory.Create();

			case PlayerStates.Jumping:
				return _jumpingFactory.Create();
		}

		throw Assert.CreateException();
	}
}
