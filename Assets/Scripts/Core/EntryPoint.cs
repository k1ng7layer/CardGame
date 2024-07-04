using System.Collections.Generic;
using Factories.StateMachineBuilder.Impl;
using Factories.StateMachineBuilderFactory;
using Helpers.Tasks.Impl;
using Services.Battle;
using Services.Deck;
using Services.EffectHandler;
using Services.GameResult;
using Services.Level;
using Services.Level.Impl;
using Services.SceneLoading;
using Services.Spawn.Impl;
using Services.Unit.Impl;
using Services.UnitRepository;
using Settings;
using UI.BattleLoose;
using UI.BattleWin;
using UI.CardPanel;
using UI.Core;
using UI.EndTurnButton;
using UI.GameWin;
using UI.PlayerEnergy;
using UI.Windows;
using UnityEngine;
using Views;

namespace Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameSettingsBase _gameSettings;
        [SerializeField] private LevelView _levelView;
        [SerializeField] private CardPanelView _cardPanelView;
        [SerializeField] private EndTurnButtonView _endTurnButtonView;
        [SerializeField] private PlayerEnergyView _playerEnergyView;
        [SerializeField] private BattleWinView _battleWinView;
        [SerializeField] private BattleLooseView _battleLooseView;
        [SerializeField] private GameWinView _gameWinView;
        [SerializeField] private CoroutineDispatcher _coroutineDispatcher;

        private BattleService _battleService;
        private UiManager _uiManager;
        private BattleDeckService _battleDeckService;
        private GameResultService _gameResultService;
        

        private void Start()
        {
            var effectHandler = new EffectFactory(
                _gameSettings.EffectSettingsBase, 
                _coroutineDispatcher);

            var spawnService = new SpawnService(_gameSettings.PrefabBase);
            
            var unitSpawnService = new UnitSpawnService(
                spawnService, 
                _gameSettings.EnemySettingsBase, 
                _gameSettings.PlayerSettings);

            var playerHolder = new PlayerUnitHolder();
            
            var enemySMBuilderFactory = new EnemyStateMachineBuilderFactory(
                _gameSettings.EnemySettingsBase, 
                effectHandler, 
                playerHolder);
            
            var playerSMBuilder = new PlayerStateMachineBuilder();
            
            _battleService = new BattleService(
                enemySMBuilderFactory,
                playerSMBuilder,
                effectHandler,
                unitSpawnService,
                _levelView,
                playerHolder);
            
            
            _battleDeckService = new BattleDeckService(
                _gameSettings.PlayerSettings.Deck, 
                _gameSettings.BattleSettingsBase,
                _battleService,
                playerHolder);
            
            _battleDeckService.Initialize();
            
            var sceneLoadingManager = new SceneLoadingManager();
            var levelService = new LevelService(_gameSettings.LevelsSettings);
            levelService.Initialize();
            
            _gameResultService = new GameResultService(_battleService, levelService);
            _gameResultService.Initialize();
            
            var uiManager = CreateUi(
                _battleDeckService, 
                _battleService, 
                playerHolder, 
                levelService, 
                sceneLoadingManager);
            
            uiManager.Initialize();
            
            _battleService.StartBattle();
        }
        

        private UiManager CreateUi(
            BattleDeckService battleDeckService, 
            BattleService battleService,
            PlayerUnitHolder playerUnitHolder,
            ILevelService levelService,
            SceneLoadingManager sceneLoadingManager
        )
        {
            var windows = new List<IUiWindow>();
            var battleWindow = new BattleWindow();
            var battleLooseWindow = new BattleLooseWindow();
            var battleWinWindow = new BattleWinWindow();
            var gameWinWindow = new GameWinWindow();
            
            windows.Add(battleWindow);
            windows.Add(battleLooseWindow);
            windows.Add(battleWinWindow);
            windows.Add(gameWinWindow);
            
            var controllers = new List<IUiController>();
            
            var cardPanelController = new CardPanelController(
                _cardPanelView, 
                battleDeckService, 
                battleService,
                playerUnitHolder,
                _gameSettings.BattleSettingsBase);

            var endTurnButtonController = new EndTurnButtonController(
                _endTurnButtonView, 
                battleService, 
                playerUnitHolder);

            var energyController = new PlayerEnergyController(_playerEnergyView, playerUnitHolder);
            var battleWinController = new BattleWinController(_battleWinView, sceneLoadingManager, levelService);
            var battleLooseController = new BattleLooseController(_battleLooseView, levelService, sceneLoadingManager);
            var gameWinController = new GameWinController(_gameWinView);
            
            controllers.Add(gameWinController);
            controllers.Add(battleWinController);
            controllers.Add(battleLooseController);
            controllers.Add(cardPanelController);
            controllers.Add(endTurnButtonController);
            controllers.Add(energyController);
            
            _uiManager = new UiManager(windows, controllers);
            
            return _uiManager;
        }

        private void OnDestroy()
        {
            _uiManager.Dispose();
            _gameResultService.Dispose();
            _battleDeckService.Dispose();
            _battleService.Dispose();
        }
    }
}