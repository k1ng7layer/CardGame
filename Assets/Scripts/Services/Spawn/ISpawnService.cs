using UnityEngine;
using Views;

namespace Services.Spawn
{
    public interface ISpawnService
    {
        IUnitView SpawnUnit(string objName, Vector3 position, Quaternion rotation);
    }
}