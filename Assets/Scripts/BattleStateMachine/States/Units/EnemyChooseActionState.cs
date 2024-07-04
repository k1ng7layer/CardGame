using Models.Effects;
using Models.Units;
using Services.EffectHandler;
using Services.UnitRepository;
using Settings.Effects;
using Settings.Units.Enemy;
using Signals;
using StateMachine;
using UnityEngine;

namespace BattleStateMachine.States.Impl
{
    public class EnemyChooseActionState : UnitStateBase
    {
        private readonly EnemySettingsBase _enemySettingsBase;
        private readonly EffectFactory _effectFactory;
        private readonly PlayerUnitHolder _playerUnitHolder;
        private Effect _chosenEffect;

        public EnemyChooseActionState(
            UnitBattleStateMachine battleStateMachine, 
            EnemySettingsBase enemySettingsBase,
            EffectFactory effectFactory,
            PlayerUnitHolder playerUnitHolder
        ) : base(battleStateMachine)
        {
            _enemySettingsBase = enemySettingsBase;
            _effectFactory = effectFactory;
            _playerUnitHolder = playerUnitHolder;
        }

        protected override void OnEnter()
        {
            var enemy = (EnemyUnit)BattleUnit;
            var settings = _enemySettingsBase.Get(enemy.EnemyType);
            var action = ChooseRandomAction(settings.EnemyEffects);
            _chosenEffect = _effectFactory.GetHandler(action);
            var player = _playerUnitHolder.PlayerUnit;
            
            _chosenEffect.Completed += FinisTurn;
            _chosenEffect.Apply(BattleUnit, player);
            
            enemy.SetExecutedEffect(action);
        }

        private EffectSettings ChooseRandomAction(EnemyBattleEffectSettings[] settingsList)
        {
            float total = 0;
  
            EffectSettings result = null;
            foreach (var settings in settingsList)
            {
                total += settings.Chance;
            }

            float randomPoint = Random.value * total;
            
            for (int i= 0; i < settingsList.Length; i++)
            {
                if (randomPoint < settingsList[i].Chance) 
                {
                    return settingsList[i].EffectSettings;
                }

                randomPoint -= settingsList[i].Chance;
            }
            
            return settingsList[^1].EffectSettings;
        }

        private void FinisTurn()
        {
            _chosenEffect.Completed -= FinisTurn;
            
            Supyrb.Signals.Get<SignalFinishTurn>().Dispatch();
        }
    }
}