using System;
using Settings.Cards;

namespace Models.Cards
{
    public class Card
    {
        public Card(CardSettings settings, CardType cardType, int id)
        {
            Settings = settings;
            CardType = cardType;
            Id = id;
        }
        
        public CardSettings Settings { get; }
        public int Cost { get; private set; }
        public bool Used { get; private set; }
        
        public event Action<Card> UsedStateChanged; 
        public CardType CardType { get; }
        public int Id { get; }

        public void SetUsed(bool value)
        {
            Used = value;
            UsedStateChanged?.Invoke(this);
        }

        public void SetCost(int value)
        {
            Cost = value;
        }
    }
}