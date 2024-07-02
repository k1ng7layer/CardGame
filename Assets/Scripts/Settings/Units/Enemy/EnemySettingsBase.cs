using System;
using Models;
using UnityEngine;

namespace Settings.Units.Enemy
{
    [CreateAssetMenu(menuName = "Settings/Enemy/EnemySettingsBase", fileName = "EnemySettingsBase")]
    public class EnemySettingsBase : ScriptableObject
    {
        [SerializeField] private EnemySettings[] _enemySettings;

        public UnitSettings Get(EnemyType enemyType)
        {
            foreach (var enemySetting in _enemySettings)
            {
                if (enemySetting.EnemyType == enemyType)
                    return enemySetting;
            }

            throw new Exception($"[{nameof(EnemySettingsBase)}] cant find settings for unit of type {enemyType}");
        }
    }
}