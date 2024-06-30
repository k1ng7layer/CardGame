using System;
using System.Collections.Generic;
using Settings.Cards;

namespace Services.Deck
{
    public class BattleDeckService
    {
        public List<CardSettings> PlayerHand { get; } = new();

        public event Action<CardSettings> Added;
        public event Action<CardSettings> Removed;
    } 
}