using Models;
using Models.Units;
using UnityEngine;

namespace Services.Unit
{
    public interface IUnitSpawnService
    {
        BattleUnit SpawnPlayer(string playerName, Vector3 position);
        EnemyUnit SpawnEnemy(EnemyType enemyType, Vector3 position);
    }
}