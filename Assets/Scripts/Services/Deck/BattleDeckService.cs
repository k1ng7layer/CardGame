using System;
using System.Collections.Generic;
using Models.Cards;
using Models.Units;
using Services.Battle;
using Services.UnitRepository;
using Settings.Battle;
using Random = UnityEngine.Random;

namespace Services.Deck
{
    public class BattleDeckService : IDisposable
    {
        private readonly Settings.Cards.Deck _playerDeck;
        private readonly BattleSettingsBase _gameSettings;
        private readonly BattleService _battleService;
        private readonly PlayerUnitHolder _playerUnitHolder;

        public BattleDeckService(
            Settings.Cards.Deck playerDeck, 
            BattleSettingsBase gameSettings,
            BattleService battleService,
            PlayerUnitHolder playerUnitHolder)
        {
            _playerDeck = playerDeck;
            _gameSettings = gameSettings;
            _battleService = battleService;
            _playerUnitHolder = playerUnitHolder;
        }

        public List<Card> PlayerHand { get; } = new();
        public List<Card> PlayedCards { get; } = new();
        public List<Card> AdditionalCards { get; } = new();

        public event Action PlayerHandChanged;

        public void Initialize()
        {
            for (int i = 0; i < _gameSettings.StarterCardsNumber; i++)
            {
                var cardIndex = Random.Range(0, _playerDeck.Cards.Count);
                var cardSettings = _playerDeck.Cards[cardIndex];
                var card = new Card(cardSettings, cardSettings.CardType, cardIndex);
                card.UsedStateChanged += HandleCardUse;
                card.SetCost(cardSettings.Cost);
                AdditionalCards.Add(card);
            }

            _battleService.BattleStarted += OnBattleStarted;
        }
        
        public void Dispose()
        {
            _battleService.BattleStarted -= OnBattleStarted;
            _battleService.CurrentBattle.NextTurnStarted -= OnNextTurnStarted;
        }

        public Card DrawCard()
        {
            if (AdditionalCards.Count == 0)
            {
                AdditionalCards.AddRange(PlayedCards);
                PlayedCards.Clear();
            }

            var card = AdditionalCards[0];
            AdditionalCards.RemoveAt(0);

            return card;
        }
        
        private void HandleCardUse(Card card)
        {
            PlayedCards.Add(card);
            PlayerHand.Remove(card);
        }

        private void OnBattleStarted()
        {
            _battleService.CurrentBattle.NextTurnStarted += OnNextTurnStarted;
            OnNextTurnStarted(_playerUnitHolder.PlayerUnit);
        }

        private void OnNextTurnStarted(BattleUnit unit)
        {
            if (unit != _playerUnitHolder.PlayerUnit)
                return;
            
            foreach (var card in PlayerHand)
            {
                PlayedCards.Add(card);
            }
            
            PlayerHand.Clear();
            
            Shuffle(AdditionalCards);
            Shuffle(PlayedCards);
            
            for (int i = 0; i < _gameSettings.StarterCardsNumber; i++)
            {
                var card = DrawCard();
                PlayerHand.Add(card);
            }
            
            PlayerHandChanged?.Invoke();
        }
        
        private void Shuffle(List<Card> cards)  
        {  
            var rng = new System.Random();
            
            int n = cards.Count;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);  
                (cards[k], cards[n]) = (cards[n], cards[k]);
            }  
        }
    } 
}