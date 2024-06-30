using Models.Units;
using Settings.Effects;
using UnityEngine;

namespace Models.Effects
{
    public class BaseAttackEffect : Effect
    {
        private readonly float _damage;
        
        public BaseAttackEffect(
            BattleUnit target, 
            ApplicationType applicationType,
            int damage
        ) : base(target, applicationType)
        {
            _damage = damage;
        }
        
        public override void Apply()
        {
            var block = Target.UnitAttributes[EffectType.Block].Value;
            var trueDamage = _damage - block;
            
            trueDamage = Mathf.Clamp(trueDamage, 0f, Mathf.Infinity);
            Target.UnitAttributes[EffectType.Block].Value -= _damage;
            Target.UnitAttributes[EffectType.Health].Value -= trueDamage;
        }
    }
}