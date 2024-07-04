using System;
using Services.Battle;
using Services.Level;
using UI.Core.Signals;
using UI.Windows;

namespace Services.GameResult
{
    public class GameResultService : IDisposable
    {
        private readonly BattleService _battleService;
        private readonly ILevelService _levelService;

        public GameResultService(BattleService battleService, ILevelService levelService)
        {
            _battleService = battleService;
            _levelService = levelService;
        }

        public void Initialize()
        {
            _battleService.BattleCompleted += OnBattleCompleted;
        }

        public void Dispose()
        {
            _battleService.BattleCompleted -= OnBattleCompleted;
        }
        
        private void OnBattleCompleted(bool isPlayerWin)
        {
            if (isPlayerWin)
                HandlePlayerWin();
            else HandlePlayerLose();
        }

        private void HandlePlayerWin()
        {
            var nextLevelName = _levelService.GetNextLevelName();
            
            if (nextLevelName == _levelService.CurrentLevel)
                Supyrb.Signals.Get<SignalOpenWindow>().Dispatch(typeof(GameWinWindow));
            else   Supyrb.Signals.Get<SignalOpenWindow>().Dispatch(typeof(BattleWinWindow));
        }

        private void HandlePlayerLose()
        {
            Supyrb.Signals.Get<SignalOpenWindow>().Dispatch(typeof(BattleLooseWindow));
        }
    }
}