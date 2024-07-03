using System;
using Models;
using Settings.Effects;
using UnityEngine;

namespace Settings.Units.Enemy
{
    [CreateAssetMenu(menuName = "Settings/Enemy/EnemySettings", fileName = "NewEnemySettings")]
    public class EnemySettings : UnitSettings
    {
        [SerializeField] private string _name;
        [SerializeField] private EnemyType enemyType;
        [SerializeField] private EnemyBattleEffectSettings[] _effects;
        
        public EnemyType EnemyType => enemyType;
        public EnemyBattleEffectSettings[] EnemyEffects => _effects;
        public string EnemyName => _name;
    }

    [Serializable]
    public class EnemyBattleEffectSettings
    {
        public float Chance;
        public EffectSettings EffectSettings;
    }
}