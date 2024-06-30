using Models.Unit;
using Models.Units;
using UnityEngine;

namespace Views
{
    public class UnitView : MonoBehaviour
    {
        public void Initialize(UnitViewModel unitViewModel)
        {
            UnitViewModel = unitViewModel;
        }

        public UnitViewModel UnitViewModel { get; private set; }
        public BattleUnit BattleUnitModel { get; private set; }
    }
}