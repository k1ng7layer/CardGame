using System.Collections.Generic;
using Factories.StateMachineBuilder.Impl;
using Factories.StateMachineBuilderFactory;
using Models;
using Models.Units;
using Services.Battle;
using Services.Deck;
using Services.EffectHandler;
using Settings.Effects;
using Settings.Enemy;
using UI.CardPanel;
using UI.Core;
using UI.Windows;
using UnityEngine;
using Views;

namespace Core
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private UnitSettingsBase unitSettingsBase;
        [SerializeField] private LevelView _levelView;
        [SerializeField] private UnitSettings _playerSettings;

        [SerializeField] private CardPanelView _cardPanelView;
        
        private readonly List<UnitView> _enemyViews = new();
        
        private void Start()
        {
            var battleDeck = new BattleDeckService();

            var effectHandler = new EffectHandler();
            
            //var effectsFactory = new EffectFactory();
            var enemySMBuilderFactory = new EnemyStateMachineBuilderFactory();
            var playerSMBuilder = new PlayerStateMachineBuilder();
            var battleService = new BattleService(
                enemySMBuilderFactory,
                playerSMBuilder,
                effectHandler);
            
            var uiManager = CreateUi(battleDeck, battleService);
            uiManager.Initialize();
            
            var player = new BattleUnit(new Dictionary<EffectType, UnitAttribute>());
            var playerView = Instantiate(_playerSettings.Prefab, 
                _levelView.PlayerSpawnTransform.position, Quaternion.identity).GetComponent<UnitView>();
            playerView.Initialize(player);
            
            var enemies = CreateEnemies();
            
            battleService.StartBattle(player, enemies);
        }
        
        private List<EnemyUnit> CreateEnemies()
        {
            var enemies = new List<EnemyUnit>();
            
            foreach (var spawnSetting in _levelView.EnemySpawnSettings)
            {
                var enemySettings = unitSettingsBase.Get(spawnSetting.Type);
                var attributes = InitializeEnemyAttributes(enemySettings);
                
                var enemy = new EnemyUnit(
                    attributes, 
                    enemySettings.Type);
                
                enemies.Add(enemy);

                var enemyView = Instantiate(enemySettings.Prefab, 
                    spawnSetting.SpawnTransform.position, Quaternion.identity).GetComponent<UnitView>();
                
                enemyView.Initialize(enemy);
                
                _enemyViews.Add(enemyView);
            }

            return enemies;
        }

        private Dictionary<EffectType, UnitAttribute> InitializeEnemyAttributes(UnitSettings unitSettings)
        {
            var attributes = new Dictionary<EffectType, UnitAttribute>
            {
                { EffectType.Block, new UnitAttribute(unitSettings.Block, 0f, 2f) },
                { EffectType.Health, new UnitAttribute(unitSettings.Health, 0f, 2f) }
            };

            return attributes;
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