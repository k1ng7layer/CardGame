using System;
using UnityEngine;

namespace Models
{
    public class UnitAttribute
    {
        private readonly float _minValue;
        private float _value;

        public UnitAttribute(
            float value, 
            float minValue, 
            float maxValue
        )
        {
            _value = value;
            _minValue = minValue;
            MaxValue = maxValue;
            BaseValue = value;
        }

        public event Action<float> ValueChanged;

        public float MaxValue { get; }
        public float BaseValue { get; }

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                _value = Mathf.Clamp(_value, _minValue, MaxValue);
                ValueChanged?.Invoke(_value);
            }
        }
    }
}