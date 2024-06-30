using System;
using System.Collections.Generic;
using Models.Effects.Buffs;
using Settings.Effects;

namespace Models.Units
{
    public class BattleUnit
    {
        public BattleUnit(Dictionary<EffectType, UnitAttribute> unitAttributes)
        {
            UnitAttributes = unitAttributes;
        }
        
        public Dictionary<EffectType, UnitAttribute> UnitAttributes { get; }
        public HashSet<AttributeBuffEffect> Effects { get; } = new();
        
        public event Action<BattleUnit> UnitDead;

        public void AddBuffEffect(AttributeBuffEffect effect)
        {
            Effects.Add(effect);
        }

        public void RemoveBuffEffect(AttributeBuffEffect effect)
        {
            Effects.Remove(effect);
        }
    }
}