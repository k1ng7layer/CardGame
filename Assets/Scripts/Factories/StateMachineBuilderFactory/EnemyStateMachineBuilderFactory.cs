using Factories.StateMachineBuilder;
using Factories.StateMachineBuilder.Impl;
using Models;
using Services.EffectHandler;
using Services.UnitRepository;
using Settings.Units.Enemy;

namespace Factories.StateMachineBuilderFactory
{
    public class EnemyStateMachineBuilderFactory
    {
        private readonly EnemySettingsBase _enemySettingsBase;
        private readonly EffectFactory _effectFactory;
        private readonly PlayerUnitHolder _playerUnitHolder;

        public EnemyStateMachineBuilderFactory(
            EnemySettingsBase enemySettingsBase, 
            EffectFactory effectFactory,
            PlayerUnitHolder playerUnitHolder
        )
        {
            _enemySettingsBase = enemySettingsBase;
            _effectFactory = effectFactory;
            _playerUnitHolder = playerUnitHolder;
        }
        
        public UnitStateMachineBuilder Create(EnemyType enemyType)
        {
            return enemyType switch
            {
                EnemyType.Type1 => new EnemyStateMachineBuilder(
                    _enemySettingsBase, 
                    _effectFactory, 
                    _playerUnitHolder),
                
                EnemyType.Type2 => new EnemyStateMachineBuilder(
                    _enemySettingsBase, 
                    _effectFactory, 
                    _playerUnitHolder)
            };
        }
    }
}