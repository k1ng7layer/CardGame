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

        public void Initialize()
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
           
        }

        private void OnCardEndDrag(CardView card)
        {
            View.EnableArrow(false);
            var cardType = card.AttachedCardSettings.CardType;
            
            var target = cardType == CardType.Attacking ? 
                View.HoveredUnit.BattleUnitModel : 
                _battleService.CurrentBattle.PlayerUnitUnit;

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
    }
}