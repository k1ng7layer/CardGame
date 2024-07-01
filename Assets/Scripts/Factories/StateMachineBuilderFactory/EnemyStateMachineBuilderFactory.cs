using Factories.StateMachineBuilder;
using Factories.StateMachineBuilder.Impl;
using Models;

namespace Factories.StateMachineBuilderFactory
{
    public class EnemyStateMachineBuilderFactory
    {
        public BattleUnitStateMachineBuilder Create(EnemyType enemyType)
        {
            return enemyType switch
            {
                EnemyType.Simple => new EnemyStateMachineBuilder(),
                EnemyType.Type1 => new EnemyStateMachineBuilder(),
                EnemyType.Type2 => new EnemyStateMachineBuilder()
            };
        }
    }
}