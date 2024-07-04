using System;
using System.Collections.Generic;
using Factories.StateMachineBuilder.Impl;
using Factories.StateMachineBuilderFactory;
using Models.Units;
using Services.EffectHandler;
using Services.Unit;
using Services.UnitRepository;
using UI.Core.Signals;
using UI.Windows;
using Views;

namespace Services.Battle
{
    public class BattleService : IDisposable
    {
        private readonly EnemyStateMachineBuilderFactory _enemyStateMachineBuilderFactory;
        private readonly PlayerStateMachineBuilder _playerStateMachineBuilder;
        private readonly EffectFactory _effectFactory;
        private readonly IUnitSpawnService _unitSpawnService;
        private readonly LevelView _levelView;
        private readonly PlayerUnitHolder _playerUnitHolder;
        private int _currentBattleID;

        public BattleService(
            EnemyStateMachineBuilderFactory enemyStateMachineBuilderFactory,
            PlayerStateMachineBuilder playerStateMachineBuilder,
            EffectFactory effectFactory,
            IUnitSpawnService unitSpawnService,
            LevelView levelView,
            PlayerUnitHolder playerUnitHolder
        )
        {
            _enemyStateMachineBuilderFactory = enemyStateMachineBuilderFactory;
            _playerStateMachineBuilder = playerStateMachineBuilder;
            _effectFactory = effectFactory;
            _unitSpawnService = unitSpawnService;
            _levelView = levelView;
            _playerUnitHolder = playerUnitHolder;
        }
        public Battle CurrentBattle { get; private set; }
        public event Action BattleStarted;
        public event Action<bool> BattleCompleted;
    
        public void StartBattle()
        {
            var enemies = InitializeEnemies();
            var player = _unitSpawnService.SpawnPlayer("Player", 
                _levelView.PlayerSpawnTransform.position);
            
            _playerUnitHolder.RegisterPlayerUnit(player);
            
            CurrentBattle = new Battle(
                _playerStateMachineBuilder, 
                _enemyStateMachineBuilderFactory, 
                player, 
                enemies, 
                _effectFactory);
            
            CurrentBattle.BattleCompleted += OnBattleCompleted;
            Supyrb.Signals.Get<SignalOpenWindow>().Dispatch(typeof(BattleWindow));
            CurrentBattle.Begin();
            BattleStarted?.Invoke();
        }
        
        public void Dispose()
        {
            CurrentBattle.BattleCompleted -= OnBattleCompleted;
            CurrentBattle.Dispose();
        }

        private void OnBattleCompleted(bool isPlayerWin)
        {
            BattleCompleted?.Invoke(isPlayerWin);
            CurrentBattle.BattleCompleted -= OnBattleCompleted;
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