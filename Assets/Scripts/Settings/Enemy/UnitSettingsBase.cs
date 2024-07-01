using System;
using Models;
using UnityEngine;

namespace Settings.Enemy
{
    [CreateAssetMenu(menuName = "Settings/Enemy/EnemySettingsBase", fileName = "EnemySettingsBase")]
    public class UnitSettingsBase : ScriptableObject
    {
        [SerializeField] private UnitSettings[] _enemySettings;

        public UnitSettings Get(EnemyType enemyType)
        {
            foreach (var enemySetting in _enemySettings)
            {
                if (enemySetting.Type == enemyType)
                    return enemySetting;
            }

            throw new Exception($"[{nameof(UnitSettingsBase)}] cant find settings for unit of type {enemyType}");
        }
    }
}