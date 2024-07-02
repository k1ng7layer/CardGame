using System.Collections.Generic;
using Factories.StateMachineBuilder.Impl;
using Factories.StateMachineBuilderFactory;
using Models.Units;
using Services.EffectHandler;
using Services.Unit;
using Supyrb;
using UI.Core.Signals;
using UI.Windows;
using Views;

namespace Services.Battle
{
    public class BattleService
    {
        private readonly EnemyStateMachineBuilderFactory _enemyStateMachineBuilderFactory;
        private readonly PlayerStateMachineBuilder _playerStateMachineBuilder;
        private readonly EffectFactory _effectFactory;
        private readonly IUnitSpawnService _unitSpawnService;
        private readonly LevelView _levelView;
        private int _currentBattleID;

        public BattleService(
            EnemyStateMachineBuilderFactory enemyStateMachineBuilderFactory,
            PlayerStateMachineBuilder playerStateMachineBuilder,
            EffectFactory effectFactory,
            IUnitSpawnService unitSpawnService,
            LevelView levelView
        )
        {
            _enemyStateMachineBuilderFactory = enemyStateMachineBuilderFactory;
            _playerStateMachineBuilder = playerStateMachineBuilder;
            _effectFactory = effectFactory;
            _unitSpawnService = unitSpawnService;
            _levelView = levelView;
        }
        public Battle CurrentBattle { get; private set; }
    
        public void StartBattle()
        {
            var enemies = InitializeEnemies();
            var player = _unitSpawnService.SpawnPlayer("Player", _levelView.PlayerSpawnTransform.position);
            
            CurrentBattle = new Battle(
                _playerStateMachineBuilder, 
                _enemyStateMachineBuilderFactory, 
                player, 
                enemies, 
                _effectFactory);
            
            CurrentBattle.BattleCompleted += OnBattleCompleted;
            CurrentBattle.Begin();
            Signals.Get<SignalOpenWindow>().Dispatch(typeof(BattleWindow));
        }

        private void OnBattleCompleted()
        {
            CurrentBattle.BattleCompleted -= OnBattleCompleted;
            Signals.Get<SignalOpenWindow>();
        }
        
        private List<EnemyUnit> InitializeEnemies()
        {
            var enemies = new List<EnemyUnit>();
            
            foreach (var enemySpawnSettings in _levelView.EnemySpawnSettings)
            {
                var enemy = _unitSpawnService.SpawnEnemy(enemySpawnSettings.Type,
                    enemySpawnSettings.SpawnTransform.position);
                
                enemies.Add(enemy);
            }

            return enemies;
        }
    }
}