using UnityEngine;
using System.Collections;

public interface IEquipment
{
	void Initialize();
	void Tick();
	void Activate();
	Vector3 GetSpawnPosition();
}
