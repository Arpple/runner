using UnityEngine;
using System.Collections;
using Zenject;

public interface IBullet
{
	void Initialize(IEquipment weapon);
	void Tick();
	bool CheckDispose();
	void Dispose();
}

public class BulletFactory : Factory<IBullet>
{
}
