using Services.Battle;
using Services.Level;
using Services.SceneLoading;
using UI.Core;

namespace UI.BattleWin
{
    public class BattleWinController : UiController<BattleWinView>
    {
        private readonly SceneLoadingManager _sceneLoadingManager;
        private readonly ILevelService _levelService;

        public BattleWinController(
            BattleWinView view, 
            SceneLoadingManager sceneLoadingManager, 
            ILevelService levelService
        ) : base(view)
        {
            _sceneLoadingManager = sceneLoadingManager;
            _levelService = levelService;
        }

        protected override void OnInitialize()
        {
            View.Initialize(StartNextBattle);
        }

        private void StartNextBattle()
        {
            _levelService.UpdateNextLevel();
            var levelName = _levelService.GetNextLevelName();
            
            _sceneLoadingManager.LoadLevel(levelName);
        }
    }
}