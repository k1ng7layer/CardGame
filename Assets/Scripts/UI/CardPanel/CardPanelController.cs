using Models.Units;
using Services.Battle;
using Services.Deck;
using Settings.Cards;
using UI.Core;

namespace UI.CardPanel
{
    public class CardPanelController : UiController<CardPanelView>
    {
        private readonly BattleDeckService _battleDeckService;
        private readonly BattleService _battleService;

        public CardPanelController(
            CardPanelView view,
            BattleDeckService battleDeckService,
            BattleService battleService
        ) : base(view)
        {
            _battleDeckService = battleDeckService;
            _battleService = battleService;
        }

        protected override void OnInitialize()
        {
            foreach (var card in _battleDeckService.PlayerHand)
            {
                View.DisplayCard(card);
            }

            View.CardBeginDrag += OnCardBeginDrag;
            View.CardEndDrag += OnCardEndDrag;
            View.CardDragged += OnCardDrag;
        }
        
        private void OnCardBeginDrag(CardView card)
        {
            View.EnableArrow(card.AttachedCardSettings.CardType == CardType.Attacking);
        }
        
        private void OnCardDrag(CardView card)
        {
            var target = GetTarget(card.AttachedCardSettings.CardType);
            
            View.SetCardUseAbility(target != null);
        }

        private void OnCardEndDrag(CardView card)
        {
            View.EnableArrow(false);
            var cardType = card.AttachedCardSettings.CardType;

            var target = GetTarget(cardType);
            
            if (target == null)
            {
                PlaceCardBack(card);
                return;
            }
                
            _battleService.CurrentBattle.ApplyCard(target, card.AttachedCardSettings);
        }

        private void PlaceCardBack(CardView cardView)
        {
            
        }
        
        private bool CanUseCard()
        {
            return false;
        }

        private BattleUnit GetTarget(CardType cardType)
        {
            var target = cardType == CardType.Attacking ? 
                View.HoveredUnit != null ? View.HoveredUnit.BattleUnitModel : null : 
                _battleService.CurrentBattle.PlayerUnitUnit;

            return target;
        }
    }
}