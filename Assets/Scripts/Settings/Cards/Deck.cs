using System.Collections.Generic;
using UnityEngine;

namespace Settings.Cards
{
    [CreateAssetMenu(menuName = "Settings/Cards/Deck", fileName = "NewDeck")]
    public class Deck : ScriptableObject
    {
        [SerializeField] private List<CardSettings> _cards;

        public List<CardSettings> Cards => _cards;
    }
}