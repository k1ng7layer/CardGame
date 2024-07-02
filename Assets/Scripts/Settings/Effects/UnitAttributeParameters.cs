using System;

namespace Settings.Effects
{
    [Serializable]
    public class UnitAttributeParameters
    {
        public EffectType AttributeType;
        public float InitialValue;
        public float MaxValue;
        public float MinValue;
    }
}