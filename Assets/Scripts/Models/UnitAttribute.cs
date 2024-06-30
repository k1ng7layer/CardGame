using System.Collections.Generic;
using Settings.Effects;

namespace Models
{
    public class UnitAttribute
    {
        private readonly HashSet<AttributeModifierSettings> _modifiers = new();
        private readonly float _baseValue;
        private readonly float _minValue;
        private readonly float _maxValue;
        private float _additionalValue;
        private bool _isDirty;

        public UnitAttribute(float baseValue, float minValue, float maxValue)
        {
            _baseValue = baseValue;
            _minValue = minValue;
            _maxValue = maxValue;
        }

        public float Value
        {
            get
            {
                if (_isDirty)
                {
                    _additionalValue = CalculateValue();
                    _isDirty = false;
                }
                
                return _baseValue + _additionalValue;
            }
            set
            {
                
            }
        }

        public void AddModifier(AttributeModifierSettings modifier)
        {
            _modifiers.Add(modifier);
            _isDirty = true;
        }

        public bool TryRemoveModifier(AttributeModifierSettings modifier)
        {
            var result = _modifiers.Remove(modifier);
            _isDirty = result;

            return result;
        }

        private float CalculateValue()
        {
            float value = 0;

            foreach (var modifier in _modifiers)
            {
                switch(modifier.OperationType)
                {
                    case OperationType.Add:
                        value += modifier.Value;
                        break;
                    case OperationType.Subtract:
                        value -= modifier.Value;
                        break;
                }
            }

            var totalValue = value + _baseValue;

            if (totalValue > _maxValue)
            {
                var excess = totalValue - _maxValue;
                value -= excess;
            }

            if (totalValue < _minValue)
            {
                var excess = totalValue - _minValue;
                value -= excess;
            }

            return value;
        }
    }
}