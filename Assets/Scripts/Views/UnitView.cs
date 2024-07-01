using Models.Units;
using UnityEngine;

namespace Views
{
    public class UnitView : MonoBehaviour
    {
        public void Initialize(BattleUnit unit)
        {
            BattleUnitModel = unit;
        }
        
        public BattleUnit BattleUnitModel { get; private set; }
    }
}