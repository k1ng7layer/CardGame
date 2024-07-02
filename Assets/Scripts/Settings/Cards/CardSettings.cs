using Settings.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Settings.Cards
{
    [CreateAssetMenu(menuName = "Settings/Cards/Card", fileName = "NewCard")]
    public class CardSettings : ScriptableObject
    {
        public CardType CardType;
        public int Cost;
        public EffectSettings Effect;
        
        [Space]
        public string Title;
        [TextArea(5, 5)]
        public string Description;
    } 
}