using Models;
using Settings.Cards;
using UnityEngine;

namespace Settings.Enemy
{
    [CreateAssetMenu(menuName = "Settings/Enemy/EnemySettings", fileName = "NewEnemySettings")]
    public class EnemySettings : ScriptableObject
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private Deck _deck;
    
        public Deck Deck => _deck;
        public EnemyType Type => _enemyType;
    }
}