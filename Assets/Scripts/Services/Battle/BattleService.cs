using System.Collections.Generic;
using Factories;
using Factories.StateMachineBuilder.Impl;
using Factories.StateMachineBuilderFactory;
using Models;
using Models.Units;
using Settings.Battle;
using Settings.Effects;

namespace Services.Battle
{
    public class BattleService
    {
        private readonly BattleSettingsBase _battleSettingsBase;
        private readonly EffectFactory _effectFactory;
        private readonly EnemyStateMachineBuilderFactory _enemyStateMachineBuilderFactory;
        private readonly PlayerStateMachineBuilder _playerStateMachineBuilder;
        private int _currentBattleID;

        public BattleService(
            BattleSettingsBase battleSettingsBase, 
            EffectFactory effectFactory,
            EnemyStateMachineBuilderFactory enemyStateMachineBuilderFactory,
            PlayerStateMachineBuilder playerStateMachineBuilder
        )
        {
            _battleSettingsBase = battleSettingsBase;
            _effectFactory = effectFactory;
            _enemyStateMachineBuilderFactory = enemyStateMachineBuilderFactory;
            _playerStateMachineBuilder = playerStateMachineBuilder;
        }
        public Battle CurrentBattle { get; private set; }
    
        public void StartNextBattle()
        {
            var player = new BattleUnit(new Dictionary<EffectType, UnitAttribute>());
            var enemies = CreateEnemies();
            
            CurrentBattle = new Battle(
                _playerStateMachineBuilder, 
                _enemyStateMachineBuilderFactory, 
                player, 
                enemies, 
                _effectFactory);
            
            CurrentBattle.BattleCompleted += OnBattleCompleted;
            CurrentBattle.Begin();
        }

        private void OnBattleCompleted()
        {
            CurrentBattle.BattleCompleted -= OnBattleCompleted;
        }

        private List<EnemyUnit> CreateEnemies()
        {
            var enemies = new List<EnemyUnit>();

            foreach (var enemySettings in _battleSettingsBase.BattleSettings.Enemies)
            {
                var enemy = new EnemyUnit(
                    new Dictionary<EffectType, UnitAttribute>(), 
                    enemySettings.Type);
                
                enemies.Add(enemy);
            }

            return enemies;
        }
    }
}