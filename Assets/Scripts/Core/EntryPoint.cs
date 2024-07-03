using System.Collections.Generic;
using Factories.StateMachineBuilder.Impl;
using Factories.StateMachineBuilderFactory;
using Helpers.Tasks.Impl;
using Services.Battle;
using Services.Deck;
using Services.EffectHandler;
using Services.Spawn.Impl;
using Services.Unit.Impl;
using Settings;
using UI.CardPanel;
using UI.Core;
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
        [SerializeField] private CoroutineDispatcher _coroutineDispatcher;

        private void Start()
        {
            var battleDeck = new BattleDeckService(_gameSettings.PlayerSettings.Deck, 
                _gameSettings.BattleSettingsBase);
            battleDeck.Initialize();
            
            var effectHandler = new EffectFactory(_gameSettings.EffectSettingsBase, _coroutineDispatcher);

            var spawnService = new SpawnService(_gameSettings.PrefabBase);
            var unitSpawnService = new UnitSpawnService(spawnService, 
                _gameSettings.EnemySettingsBase, 
                _gameSettings.PlayerSettings);
            
            var enemySMBuilderFactory = new EnemyStateMachineBuilderFactory();
            var playerSMBuilder = new PlayerStateMachineBuilder();
            
            var battleService = new BattleService(
                enemySMBuilderFactory,
                playerSMBuilder,
                effectHandler,
                unitSpawnService,
                _levelView);
            
            
            var uiManager = CreateUi(battleDeck, battleService);
            uiManager.Initialize();
            
            battleService.StartBattle();
        }
        

        private UiManager CreateUi(BattleDeckService battleDeckService, BattleService battleService)
        {
            var windows = new List<IUiWindow>();
            var battleWindow = new BattleWindow();
            windows.Add(battleWindow);
            var controllers = new List<IUiController>();
            
            var cardPanelController = new CardPanelController(_cardPanelView, battleDeckService, battleService);
            controllers.Add(cardPanelController);
            var uiManager = new UiManager(windows, controllers);
            
            return uiManager;
        }
    }
}