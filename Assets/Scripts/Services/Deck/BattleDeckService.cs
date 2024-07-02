using System;
using System.Collections.Generic;
using Settings.Battle;
using Settings.Cards;

namespace Services.Deck
{
    public class BattleDeckService
    {
        private readonly Settings.Cards.Deck _playerDeck;
        private readonly BattleSettingsBase _gameSettings;

        public BattleDeckService(
            Settings.Cards.Deck playerDeck, 
            BattleSettingsBase gameSettings)
        {
            _playerDeck = playerDeck;
            _gameSettings = gameSettings;
        }

        public List<CardSettings> PlayerHand { get; } = new();
        public List<CardSettings> Played { get; } = new();
        public List<CardSettings> AdditionalDeck { get; } = new();

        public event Action<CardSettings> Added;
        public event Action<CardSettings> Removed;

        public void Initialize()
        {
            for (int i = 0; i < _gameSettings.StarterCardsNumber; i++)
            {
                var cardIndex = UnityEngine.Random.Range(0, _playerDeck.Cards.Count);
                var card = _playerDeck.Cards[cardIndex];
                PlayerHand.Add(card);
            }
        }
    } 
}