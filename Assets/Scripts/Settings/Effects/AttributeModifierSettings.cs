using UnityEngine;

namespace Settings.Effects
{
    [CreateAssetMenu(menuName = "Settings/Effects/AttributeModifier", fileName = "NewModifier")]
    public class AttributeModifierSettings : ScriptableObject
    {
        public EAttributeType Attribute;
        public OperationType OperationType;
        public int Value;
    }
}