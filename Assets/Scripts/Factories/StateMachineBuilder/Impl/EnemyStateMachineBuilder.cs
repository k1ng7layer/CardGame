using System.Collections.Generic;
using BattleStateMachine.States;
using BattleStateMachine.States.Impl;
using BattleStateMachine.States.Units;
using Services.EffectHandler;
using Services.UnitRepository;
using Settings.Units.Enemy;
using StateMachine;

namespace Factories.StateMachineBuilder.Impl
{
    public class EnemyStateMachineBuilder : UnitStateMachineBuilder
    {
        private readonly EnemySettingsBase _enemySettingsBase;
        private readonly EffectFactory _effectFactory;
        private readonly PlayerUnitHolder _playerUnitHolder;

        public EnemyStateMachineBuilder(
            EnemySettingsBase enemySettingsBase, 
            EffectFactory effectFactory,
            PlayerUnitHolder playerUnitHolder
        )
        {
            _enemySettingsBase = enemySettingsBase;
            _effectFactory = effectFactory;
            _playerUnitHolder = playerUnitHolder;
        }
        
        protected override void ComposeStates(List<StateBase> stateList, UnitBattleStateMachine battleStateMachine)
        {
            battleStateMachine.AddState(new OnTurnFinishState(battleStateMachine));
            battleStateMachine.AddState(new EnemyStartTurnState(battleStateMachine));
            
            battleStateMachine.AddState(new EnemyChooseActionState(
                battleStateMachine, 
                _enemySettingsBase, 
                _effectFactory, 
                _playerUnitHolder));
        }
    }
}