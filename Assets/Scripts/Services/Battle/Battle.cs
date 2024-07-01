using System;
using System.Collections.Generic;
using Factories.StateMachineBuilder;
using Factories.StateMachineBuilderFactory;
using Models.Effects;
using Models.Units;
using Services.Battle.StateMachine;
using Services.Battle.StateMachine.States.Impl;
using Settings.Cards;
using Settings.Effects;

namespace Services.Battle
{
    public class Battle
    {
        private readonly BattleUnitStateMachineBuilder _playerStateMachineBuilder;
        private readonly BattleUnitStateMachineBuilder _enemyStateMachineBuilder;
        private readonly EnemyStateMachineBuilderFactory _enemyStateMachineBuilderFactory;
        private readonly EffectHandler.EffectHandler _effectHandler;
        private readonly List<BattleUnit> _turnQueue = new();
        private readonly Dictionary<BattleUnit, UnitStateMachine> _stateMachines = new();
        private readonly List<EnemyUnit> _enemies;
        private BattleUnit _currentBattleUnit;

        public Battle(
            BattleUnitStateMachineBuilder playerStateMachineBuilder,
            EnemyStateMachineBuilderFactory enemyStateMachineBuilderFactory,
            BattleUnit playerUnit, 
            List<EnemyUnit> enemies, 
            EffectHandler.EffectHandler effectHandler)
        {
            _playerStateMachineBuilder = playerStateMachineBuilder;
            _enemyStateMachineBuilderFactory = enemyStateMachineBuilderFactory;
            _enemies = enemies;
            _effectHandler = effectHandler;
            PlayerUnitUnit = playerUnit;
        }
        
        public BattleUnit PlayerUnitUnit { get; private set; }

        public event Action BattleCompleted;

        public void Begin()
        {
            InitializeStateMachines();
            InitializeTurnQueue();
            BeginNextTurn();
        }

        public void EndTurn()
        {
            BeginNextTurn();
        }
        
        public void ApplyCard(BattleUnit target, CardSettings cardSettings)
        {
            var stateMachine = _stateMachines[_currentBattleUnit];

            ApplyEffect(target, cardSettings.Effect);
            
            if (cardSettings.CardType == CardType.Attacking)
                AttackTarget(target, 0);
        }

        public void ApplyEffect(
            BattleUnit target, 
            EffectSettings effectSettings
        )
        {
            // var effect = _effectFactory.CreateBuffEffect(target, 
            //         effectSettings.LifetimeType, 
            //         effectSettings);
            //
            // if (effectSettings.ApplicationType == ApplicationType.Instant 
            //     && effectSettings.LifetimeType == LifetimeType.Permanent)
            // {
            //     effect.Apply(PlayerUnitUnit, target);
            // }
            //     
            // target.AddBuffEffect(effect);
        }

        public void AttackTarget(BattleUnit target, int damage)
        {
            var effect = _effectHandler.GetHandler<BaseAttackEffect>();
            
            effect.Apply(PlayerUnitUnit, target);
        }

        private void BeginNextTurn()
        {
            if (_currentBattleUnit != null)
            {
                _stateMachines[_currentBattleUnit].ChangeState<OnTurnFinishState>();
                _currentBattleUnit = GetNextUnit();
                _stateMachines[_currentBattleUnit].ChangeState<OnTurnStartState>();
            }
            else
            {
                _currentBattleUnit = GetNextUnit();
                _stateMachines[_currentBattleUnit].ChangeState<OnTurnStartState>();
            }
        }

        private void RemoveDeadUnit(BattleUnit battleUnit)
        {
            battleUnit.UnitDead -= RemoveDeadUnit;
            
            _turnQueue.Remove(battleUnit);
        }

        private BattleUnit GetNextUnit()
        {
            var unit = _turnQueue[0];
            _turnQueue.RemoveAt(0);

            return unit;
        }

        private void InitializeStateMachines()
        {
            var playerStateMachine = _playerStateMachineBuilder.Build(PlayerUnitUnit);
            _stateMachines.Add(PlayerUnitUnit, playerStateMachine);

            foreach (var enemy in _enemies)
            {
                var stateMachine = _enemyStateMachineBuilderFactory
                    .Create(enemy.EnemyType)
                    .Build(enemy);
                
                _stateMachines.Add(enemy, stateMachine);
            }
        }

        private void InitializeTurnQueue()
        {
            _turnQueue.Add(PlayerUnitUnit);
            PlayerUnitUnit.UnitDead += RemoveDeadUnit;
            
            foreach (var enemy in _enemies)
            {
                _turnQueue.Add(enemy);
                enemy.UnitDead += RemoveDeadUnit;
            }
        }
    }
}