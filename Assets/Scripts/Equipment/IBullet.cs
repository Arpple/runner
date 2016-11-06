using UnityEngine;
using System.Collections;
using Zenject;

public interface IBullet
{
	void Initialize(IEquipment weapon);
	void Tick();
	bool CheckDispose();
	void Dispose();
	void SetPosition(Vector3 position);
}

public class BulletFactory : Factory<IBullet>
{
}
