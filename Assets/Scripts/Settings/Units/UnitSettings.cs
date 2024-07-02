using Settings.Effects;
using UnityEngine;

namespace Settings.Units
{
    [CreateAssetMenu(menuName = "Settings/Enemy/UnitSettings", fileName = "NewUnitSettings")]
    public abstract class UnitSettings : ScriptableObject
    {
        [SerializeField] private UnitAttributeParameters[] _attributeParameters;

        public UnitAttributeParameters[] AttributeParameters => _attributeParameters;
    }
}