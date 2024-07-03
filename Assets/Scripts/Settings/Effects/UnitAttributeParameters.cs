using System;

namespace Settings.Effects
{
    [Serializable]
    public class UnitAttributeParameters
    {
        public EAttributeType AttributeType;
        public float InitialValue;
        public float MaxValue;
        public float MinValue;
    }
}