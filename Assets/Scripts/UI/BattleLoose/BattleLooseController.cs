using Services.Level;
using Services.SceneLoading;
using UI.Core;

namespace UI.BattleLoose
{
    public class BattleLooseController : UiController<BattleLooseView>
    {
        private readonly ILevelService _levelService;
        private readonly SceneLoadingManager _sceneLoadingManager;

        public BattleLooseController(
            BattleLooseView view, 
            ILevelService levelService,
            SceneLoadingManager sceneLoadingManager
        ) : base(view)
        {
            _levelService = levelService;
            _sceneLoadingManager = sceneLoadingManager;
        }

        protected override void OnInitialize()
        {
            View.Initialize(RestartBattle);
        }

        private void RestartBattle()
        {
            _sceneLoadingManager.LoadLevel(_levelService.CurrentLevel);
        }
    }
}