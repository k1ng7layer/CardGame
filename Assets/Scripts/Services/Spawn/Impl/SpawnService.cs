using Settings.Prefab;
using UnityEngine;
using Views;

namespace Services.Spawn.Impl
{
    public class SpawnService : ISpawnService
    {
        private readonly PrefabBase _prefabBase;

        public SpawnService(PrefabBase prefabBase)
        {
            _prefabBase = prefabBase;
        }
        
        public IUnitView SpawnUnit(string objName, Vector3 position, Quaternion rotation)
        {
            var prefab = _prefabBase.Get(objName);
            var obj = Object.Instantiate(prefab, position, rotation);
            var view = obj.GetComponent<IUnitView>();

            return view;
        }
    }
}