using Models.Units;
using Settings.Effects;
using UnityEngine;

namespace Models.Effects
{
    public class BaseAttackEffect : Effect
    {
        private readonly float _damage;
        
        public BaseAttackEffect(EffectSettings settings) : base(settings)
        {
        }
        
        public override void Apply(BattleUnit user, BattleUnit target)
        {
            var block = target.UnitAttributes[EffectType.Block].Value;
            var trueDamage = _damage - block;
            
            trueDamage = Mathf.Clamp(trueDamage, 0f, Mathf.Infinity);
            target.UnitAttributes[EffectType.Block].Value -= _damage;
            target.UnitAttributes[EffectType.Health].Value -= trueDamage;
        }
    }
}