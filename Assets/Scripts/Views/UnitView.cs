using Models.Units;
using UnityEngine;

namespace Views
{
    public class UnitView : MonoBehaviour, IUnitView
    {
        public void Initialize(BattleUnit unit)
        {
            BattleUnitModel = unit;
        }
        
        public BattleUnit BattleUnitModel { get; private set; }
    }
}