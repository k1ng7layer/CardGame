using System.Collections.Generic;
using Settings.Effects;

namespace Models.Units
{
    public class EnemyUnit : BattleUnit
    {
        public EnemyUnit(
            Dictionary<EAttributeType, UnitAttribute> unitAttributes, 
            EnemyType enemyType) : base(unitAttributes
        )
        {
            EnemyType = enemyType;
        }
        
        public EnemyType EnemyType { get; }
    }
}