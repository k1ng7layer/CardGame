using Settings.Units;
using UnityEngine;

namespace Settings.Battle
{
    [CreateAssetMenu(menuName = "Settings/Battle/BattleSettings", fileName = "NewBattleSettings")]
    public class BattleSettings : ScriptableObject
    {
        [SerializeField] private UnitSettings[] _enemies;

        public UnitSettings[] Enemies => _enemies;
    }
}