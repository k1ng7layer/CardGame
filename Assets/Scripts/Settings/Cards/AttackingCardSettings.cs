using UnityEngine;

namespace Settings.Cards
{
    [CreateAssetMenu(menuName = "Settings/Cards/AttackingCard", fileName = "NewCard")]
    public class AttackingCardSettings : CardSettings
    {
        public int Damage;
    }
}