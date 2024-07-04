using System;
using System.Collections.Generic;
using Models.Effects;
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
        public HashSet<Buff.Buff> Buffs { get; } = new();
        public Vector3 Position { get; private set; }
        public bool InAction { get; private set; }
        public bool IsDead { get; private set; }
        public event Action<BattleUnit> Dead;
        public event Action<Vector3> PositionChanged;
        public event Action<bool> AttackPerformed;
        public event Action<bool> InActionStatusChanged;
        public event Action<EffectSettings> EffectExcecuted;

        public void AddBuffEffect(Buff.Buff effect)
        {
            Buffs.Add(effect);
        }

        public void RemoveBuffEffect(Buff.Buff effect)
        {
            Buffs.Remove(effect);
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

        public void SetInAction(bool value)
        {
            InAction = value;
            InActionStatusChanged?.Invoke(value);
        }

        public void SetDead(bool value)
        {
            IsDead = value;
            Dead?.Invoke(this);
        }

        public void SetExecutedEffect(EffectSettings effectSettings)
        {
            EffectExcecuted?.Invoke(effectSettings);
        }
    }
}