using System.Collections;
using Helpers.Tasks;
using Models.Units;
using Settings.Effects;
using UnityEngine;

namespace Models.Effects
{
    public class BaseAttackEffect : Effect
    {
        private static readonly Vector2 Offset = new Vector2(-3f, -3f);

        private readonly EffectSettings _settings;
        private Vector3 _initialPosition;
        
        public BaseAttackEffect(EffectSettings settings, 
            ICoroutineDispatcher coroutineDispatcher
        ) : base(settings, coroutineDispatcher)
        {
            _settings = settings;
        }
        
        public override void Apply(BattleUnit attacker, BattleUnit target)
        {
            _initialPosition = attacker.Position;
            var dirVector = target.Position - attacker.Position;
            
            var dirNorm = dirVector.normalized;
            
            var targetPos = new Vector3(target.Position.x + dirNorm.x * Offset.x, target.Position.y + dirNorm.y * Offset.y,
                target.Position.z);
            
            CoroutineDispatcher.RunCoroutine(MoveUnitToTarget(attacker, targetPos), () =>
            {
                OnUnitReachedTarget(attacker, target);
            });
        }

        private void OnUnitReachedTarget(BattleUnit attacker, BattleUnit target)
        {
            PerformAttack(attacker, target);
            CoroutineDispatcher.Delay(0.4f, () => { MoveBack(attacker);});
        }

        private IEnumerator MoveUnitToTarget(BattleUnit attacker, Vector3 position)
        {
            while ((position - attacker.Position).sqrMagnitude >= 0.0001f)
            {
                var dir = (position - attacker.Position).normalized;
                var movement = attacker.Position + dir * Time.deltaTime * 10f;
                attacker.SetPosition(movement);

                yield return null;
            }
        }

        private void PerformAttack(BattleUnit attacker, BattleUnit target)
        {
            attacker.SetAttackState(true);
            var block = target.UnitAttributes[EAttributeType.Block].Value;
            var trueDamage = _settings.Damage - block;
            
            trueDamage = Mathf.Clamp(trueDamage, 0f, Mathf.Infinity);
            target.UnitAttributes[EAttributeType.Block].Value -= _settings.Damage;
            target.UnitAttributes[EAttributeType.Health].Value -= trueDamage;
        }

        private void MoveBack(BattleUnit unit)
        {
            CoroutineDispatcher.RunCoroutine(MoveUnitToTarget(unit, _initialPosition));
        }
    }
}