using System.Collections.Generic;
using Factories.StateMachineBuilder.Impl;
using Factories.StateMachineBuilderFactory;
using Models.Units;
using Supyrb;
using UI.Core.Signals;
using UI.Windows;

namespace Services.Battle
{
    public class BattleService
    {
        private readonly EnemyStateMachineBuilderFactory _enemyStateMachineBuilderFactory;
        private readonly PlayerStateMachineBuilder _playerStateMachineBuilder;
        private readonly EffectHandler.EffectHandler _effectHandler;
        private int _currentBattleID;

        public BattleService(
            EnemyStateMachineBuilderFactory enemyStateMachineBuilderFactory,
            PlayerStateMachineBuilder playerStateMachineBuilder,
            EffectHandler.EffectHandler effectHandler
        )
        {
            _enemyStateMachineBuilderFactory = enemyStateMachineBuilderFactory;
            _playerStateMachineBuilder = playerStateMachineBuilder;
            _effectHandler = effectHandler;
        }
        public Battle CurrentBattle { get; private set; }
    
        public void StartBattle(BattleUnit player, List<EnemyUnit> enemies)
        {
            CurrentBattle = new Battle(
                _playerStateMachineBuilder, 
                _enemyStateMachineBuilderFactory, 
                player, 
                enemies, 
                _effectHandler);
            
            CurrentBattle.BattleCompleted += OnBattleCompleted;
            CurrentBattle.Begin();
            Signals.Get<SignalOpenWindow>().Dispatch(typeof(BattleWindow));
        }

        private void OnBattleCompleted()
        {
            CurrentBattle.BattleCompleted -= OnBattleCompleted;
            var wnd = typeof(BattleWinWindow);
            Signals.Get<SignalOpenWindow>();
        }
    }
}