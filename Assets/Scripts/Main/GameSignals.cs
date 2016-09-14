using System;
using Zenject;

public static class Signals
{
	public class PlayerDead : Signal
	{
		public class Trigger : TriggerBase
		{}
	}
}
