using Models;
using Settings.Cards;
using UnityEngine;

namespace Settings.Enemy
{
    [CreateAssetMenu(menuName = "Settings/Enemy/UnitSettings", fileName = "NewUnitSettings")]
    public class UnitSettings : ScriptableObject
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private Deck _deck;
        [SerializeField] private GameObject _prefab;
    
        public Deck Deck => _deck;
        public EnemyType Type => _enemyType;
        public GameObject Prefab => _prefab;
        public int Block;
        public int Health;
    }
}