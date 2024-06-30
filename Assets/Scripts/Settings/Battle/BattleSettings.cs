using Settings.Enemy;
using UnityEngine;

namespace Settings.Battle
{
    [CreateAssetMenu(menuName = "Settings/Battle/BattleSettings", fileName = "NewBattleSettings")]
    public class BattleSettings : ScriptableObject
    {
        [SerializeField] private EnemySettings[] _enemies;

        public EnemySettings[] Enemies => _enemies;
    }
}