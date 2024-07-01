using System;
using Models;
using UnityEngine;

namespace Settings.Enemy
{
    [Serializable]
    public class EnemySpawnSettings
    {
        public EnemyType Type;
        public Transform SpawnTransform;
    }
}