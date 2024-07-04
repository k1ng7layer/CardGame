using System;
using System.Collections.Generic;
using BattleStateMachine.States.Impl;
using BattleStateMachine.States.Units;
using Factories.StateMachineBuilder;
using Factories.StateMachineBuilderFactory;
using Models.Cards;
using Models.Units;
using Settings.Effects;
using Signals;
using StateMachine;
using Supyrb;

namespace Services.Battle
{
    public class Battle : IDisposable
    {
        private readonly UnitStateMachineBuilder _playerStateMachineBuilder;
        private readonly UnitStateMachineBuilder _enemyStateMachineBuilder;
        private readonly EnemyStateMachineBuilderFactory _enemyStateMachineBuilderFactory;
        private readonly EffectHandler.EffectFactory _effectFactory;
        private readonly List<BattleUnit> _turnQueue = new();
        private readonly Dictionary<BattleUnit, UnitBattleStateMachine> _stateMachines = new();
        private readonly List<EnemyUnit> _enemies;
        private BattleUnit _currentBattleUnit;

        public Battle(
            UnitStateMachineBuilder playerStateMachineBuilder,
            EnemyStateMachineBuilderFactory enemyStateMachineBuilderFactory,
            BattleUnit playerUnit, 
            List<EnemyUnit> enemies, 
            EffectHandler.EffectFactory effectFactory)
        {
            _playerStateMachineBuilder = playerStateMachineBuilder;
            _enemyStateMachineBuilderFactory = enemyStateMachineBuilderFactory;
            _enemies = enemies;
            _effectFactory = effectFactory;
            PlayerUnit = playerUnit;
        }
        
        public BattleUnit PlayerUnit { get; }
        public event Action<bool> BattleCompleted;
        public event Action<BattleUnit> NextTurnStarted;

        public void Begin()
        {
            Supyrb.Signals.Get<SignalFinishTurn>().AddListener(BeginNextTurn);
            
            InitializeUnits();
            InitializeStateMachines();
            InitializeTurnQueue();
            BeginNextTurn();
        }
        
        public void Dispose()
        {
            Supyrb.Signals.Get<SignalFinishTurn>().RemoveListener(BeginNextTurn);
            
            PlayerUnit.Dead -= OnUnitDead;
            
            foreach (var enemy in _enemies)
            {
                enemy.Dead -= OnUnitDead;
            }
            
            _enemies.Clear();
        }

        public void EndTurn()
        {
            BeginNextTurn();
        }
        
        public void ApplyCard(BattleUnit target, Card card)
        {
            PlayerUnit.UnitAttributes[EAttributeType.Energy].Value -= card.Cost;
            card.SetUsed(true);
            ApplyEffect(target, card.Settings.Effect);
        }

        public void ApplyEffect(
            BattleUnit target, 
            EffectSettings effectSettings
        )
        {
            var effectHandler = _effectFactory.GetHandler(effectSettings);
            
            effectHandler.Apply(PlayerUnit, target);
        }

        private void BeginNextTurn()
        {
            if (_currentBattleUnit != null)
            {
                _stateMachines[_currentBattleUnit].ChangeState<OnTurnFinishState>();
                _turnQueue.Add(_currentBattleUnit);
            }
            
            _currentBattleUnit = GetNextUnit();
            
            if (_currentBattleUnit == PlayerUnit)
                HandlePlayerTurn();
            else HandleEnemyTurn();
            
            NextTurnStarted?.Invoke(_currentBattleUnit);
        }

        private void HandlePlayerTurn()
        {
            _stateMachines[_currentBattleUnit].ChangeState<PlayerStartTurnState>();
        }

        private void HandleEnemyTurn()
        {
            _stateMachines[_currentBattleUnit].ChangeState<EnemyStartTurnState>();
        }

        private void OnUnitDead(BattleUnit deadUnit)
        {
            deadUnit.Dead -= OnUnitDead;
            _turnQueue.Remove(deadUnit);

            if (deadUnit == PlayerUnit)
            {
                BattleCompleted?.Invoke(false);
            }
            else
            {
                _enemies.Remove((EnemyUnit)deadUnit);
                
                if (_enemies.Count == 0)
                    BattleCompleted?.Invoke(true);
            }
        }

        private BattleUnit GetNextUnit()
        {
            var unit = _turnQueue[0];
            _turnQueue.RemoveAt(0);

            return unit;
        }

        private void InitializeStateMachines()
        {
            var playerStateMachine = _playerStateMachineBuilder.Build(PlayerUnit);
            _stateMachines.Add(PlayerUnit, playerStateMachine);

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
            _turnQueue.Add(PlayerUnit);
            PlayerUnit.Dead += OnUnitDead;
            
            foreach (var enemy in _enemies)
            {
                _turnQueue.Add(enemy);
                enemy.Dead += OnUnitDead;
            }
        }

        private void InitializeUnits()
        {
            PlayerUnit.UnitAttributes[EAttributeType.Health].ValueChanged += value =>
            {
                CheckUnitHealth(value, PlayerUnit);
            };
                
            foreach (var enemy in _enemies)
            {
                enemy.UnitAttributes[EAttributeType.Health].ValueChanged += value =>
                {
                    CheckUnitHealth(value, enemy);
                };
            }
        }

        private void CheckUnitHealth(float value, BattleUnit battleUnit)
        {
            if (value <= 0)
                battleUnit.SetDead(true);
        }
    }
}