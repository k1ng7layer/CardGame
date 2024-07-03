using System;
using System.Collections.Generic;
using Models.Effects.Buffs;
using Settings.Effects;
using UnityEngine;

namespace Models.Units
{
    public class BattleUnit
    {
        public BattleUnit(Dictionary<EAttributeType, UnitAttribute> unitAttributes)
        {
            UnitAttributes = unitAttributes;
        }
        
        public Dictionary<EAttributeType, UnitAttribute> UnitAttributes { get; }
        public HashSet<AttributeBuffEffect> Effects { get; } = new();
        public Vector3 Position { get; private set; }
        
        public event Action<BattleUnit> UnitDead;
        public event Action<Vector3> PositionChanged;
        public event Action<bool> AttackPerformed;

        public void AddBuffEffect(AttributeBuffEffect effect)
        {
            Effects.Add(effect);
        }

        public void RemoveBuffEffect(AttributeBuffEffect effect)
        {
            Effects.Remove(effect);
        }

        public void SetPosition(Vector3 position)
        {
            Position = position;
            PositionChanged?.Invoke(Position);
        }

        public void SetAttackState(bool value)
        {
            AttackPerformed?.Invoke(value);
        }
    }
}