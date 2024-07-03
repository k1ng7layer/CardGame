using System.Collections.Generic;
using Models;
using Models.Units;
using Services.Spawn;
using Settings.Effects;
using Settings.Units;
using Settings.Units.Enemy;
using Settings.Units.Player;
using UnityEngine;

namespace Services.Unit.Impl
{
    public class UnitSpawnService : IUnitSpawnService
    {
        private readonly EnemySettingsBase _enemySettingsBase;
        private readonly PlayerSettings _playerSettings;
        private readonly ISpawnService _spawnService;

        public UnitSpawnService(ISpawnService spawnService, 
            EnemySettingsBase enemySettingsBase, 
            PlayerSettings playerSettings
        )
        {
            _spawnService = spawnService;
            _enemySettingsBase = enemySettingsBase;
            _playerSettings = playerSettings;
        }

        public BattleUnit SpawnPlayer(string playerName, Vector3 position)
        {
            var view = _spawnService.SpawnUnit(playerName, position, Quaternion.identity);
            var attributes = InitializeAttributes(_playerSettings);
            var unit = new BattleUnit(attributes);
            unit.SetPosition(position);
            view.Initialize(unit);

            return unit;
        }

        public EnemyUnit SpawnEnemy(EnemyType enemyType, Vector3 position)
        {
            var view = _spawnService.SpawnUnit(enemyType.ToString(), position, Quaternion.identity);
            var enemySettings = _enemySettingsBase.Get(enemyType);
            var attributes = InitializeAttributes(enemySettings);
            var enemyUnit = new EnemyUnit(attributes, enemyType);
            enemyUnit.SetPosition(position);
            view.Initialize(enemyUnit);
            
            return enemyUnit;
        }
        
        private Dictionary<EAttributeType, UnitAttribute> InitializeAttributes(UnitSettings unitSettings)
        {
            var attributes = new Dictionary<EAttributeType, UnitAttribute>();
            
            foreach (var setting in unitSettings.AttributeParameters)
            {
                attributes.Add(setting.AttributeType, new UnitAttribute(setting.InitialValue, setting.MinValue, setting.MaxValue));
            }
            
            return attributes;
        }
    }
}