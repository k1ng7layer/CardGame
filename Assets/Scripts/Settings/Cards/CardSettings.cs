using Settings.Effects;
using UnityEngine;

namespace Settings.Cards
{
    [CreateAssetMenu(menuName = "Settings/Cards/Card", fileName = "NewCard")]
    public class CardSettings : ScriptableObject
    {
        public CardType CardType;
        public int Cost;
        public EffectSettings Effect;
    } 
}