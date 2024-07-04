using Models.Cards;
using Models.Units;
using Services.Battle;
using Services.Deck;
using Services.UnitRepository;
using Settings.Battle;
using Settings.Cards;
using Settings.Effects;
using UI.Core;

namespace UI.CardPanel
{
    public class CardPanelController : UiController<CardPanelView>
    {
        private readonly BattleDeckService _battleDeckService;
        private readonly BattleService _battleService;
        private readonly PlayerUnitHolder _playerUnitHolder;
        private readonly BattleSettingsBase _battleSettingsBase;

        public CardPanelController(
            CardPanelView view,
            BattleDeckService battleDeckService,
            BattleService battleService,
            PlayerUnitHolder playerUnitHolder,
            BattleSettingsBase battleSettingsBase
        ) : base(view)
        {
            _battleDeckService = battleDeckService;
            _battleService = battleService;
            _playerUnitHolder = playerUnitHolder;
            _battleSettingsBase = battleSettingsBase;
        }

        protected override void OnInitialize()
        {
            View.CardBeginDrag += OnCardBeginDrag;
            View.CardEndDrag += OnCardEndDrag;
            View.CardDragged += OnCardDrag;
        }

        protected override void OnShow()
        {
            var energy =  _playerUnitHolder.PlayerUnit.UnitAttributes[EAttributeType.Energy];
            energy.ValueChanged += OnEnergyUpdated;
            _battleDeckService.PlayerHandChanged += SpawnHand;
            OnEnergyUpdated(energy.Value);
        }
        
        public override void Dispose()
        {
            _playerUnitHolder.PlayerUnit.UnitAttributes[EAttributeType.Energy].ValueChanged -= OnEnergyUpdated;
        }

        private void SpawnHand()
        {
            DespawnCurrentCards();
            
            var energy =  _playerUnitHolder.PlayerUnit.UnitAttributes[EAttributeType.Energy];
            
            foreach (var card in _battleDeckService.PlayerHand)
            {
                card.UsedStateChanged += OnCardUsed;
                View.DisplayCard(card, energy.Value >= card.Cost);
            }
        }

        private void OnTurnStarted(BattleUnit battleUnit)
        {
            if (battleUnit == _playerUnitHolder.PlayerUnit)
            {
                DespawnCurrentCards();
                DrawCards();
                View.Show();
            }
        }

        private void DrawCards()
        {
            var energy =  _playerUnitHolder.PlayerUnit.UnitAttributes[EAttributeType.Energy];

            for (var index = 0; index < _battleSettingsBase.StarterCardsNumber; index++)
            {
                var card = _battleDeckService.DrawCard();
                card.UsedStateChanged += OnCardUsed;
                View.DisplayCard(card, energy.Value >= card.Cost);
            }
        }

        private void DespawnCurrentCards()
        {
            foreach (var cardEntry in View.DisplayedCards)
            {
                var card = cardEntry.Key;
                card.UsedStateChanged -= OnCardUsed;
            }
            
            View.DespawnCurrentCards();
        }

        private void OnCardUsed(Card card)
        {
            card.UsedStateChanged -= OnCardUsed;
            View.DestroyCard(card);
        }

        private void OnEnergyUpdated(float value)
        {
            foreach (var cardEntry in View.DisplayedCards)
            {
                cardEntry.Value.SetInteractable(value >= cardEntry.Value.AttachedCard.Cost);
            }
        }

        private void OnCardBeginDrag(CardView card)
        {
            View.EnableArrow(card.AttachedCard.CardType == CardType.Attacking);
        }
        
        private void OnCardDrag(CardView card)
        {
            var target = GetTarget(card.AttachedCard.CardType);
            
            View.SetCardUseAbility(target != null);
        }

        private void OnCardEndDrag(CardView card)
        {
            View.EnableArrow(false);
            var cardType = card.AttachedCard.CardType;

            var target = GetTarget(cardType);
            
            if (target == null)
                return;
                
            _battleService.CurrentBattle.ApplyCard(target, card.AttachedCard);
        }

        private BattleUnit GetTarget(CardType cardType)
        {
            var target = cardType == CardType.Attacking ? 
                View.HoveredUnit != null ? View.HoveredUnit.BattleUnitModel : null : 
                _battleService.CurrentBattle.PlayerUnit;

            return target;
        }
    }
}