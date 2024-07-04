using System;
using System.Collections.Generic;
using Models.Cards;
using UI.Arrow;
using UI.Core;
using UnityEngine;
using Views;

namespace UI.CardPanel
{
    public class CardPanelView : UiView
    {
        [SerializeField] private CardView _cardViewPrefab;
        [SerializeField] private Transform _cardListRoot;
        [SerializeField] private BezierArrows _aimArrow;
        
        private bool _dragging;
        
        public event Action<CardView> CardBeginDrag;
        public event Action<CardView> CardDragged;
        public event Action<CardView> CardEndDrag;
        
        public UnitView HoveredUnit { get; private set; }
        public Dictionary<Card, CardView> DisplayedCards { get; private set; } = new();
        
        public void SetCardUseAbility(bool value)
        {
            _aimArrow.CanUse(value);
        }

        public void DisplayCard(Card card, bool interactable)
        {
            var cardView = Instantiate(_cardViewPrefab, _cardListRoot);
            cardView.Initialize(card);
            cardView.CardDragged += OnCardDrag;
            cardView.CardBeginDrag += OnCardBeginDrag;
            cardView.CardEndDrag += OnCardEndDrag;
            cardView.SetInteractable(interactable);
            
            DisplayedCards.Add(card, cardView);
        }

        public void DestroyCard(Card card)
        {
            var cardView = DisplayedCards[card];
            cardView.CardDragged -= OnCardDrag;
            cardView.CardBeginDrag -= OnCardBeginDrag;
            cardView.CardEndDrag -= OnCardEndDrag;
            DisplayedCards.Remove(card);
            Destroy(cardView.gameObject);
        }

        public void DespawnCurrentCards()
        {
            foreach (var cardEntry in DisplayedCards)
            {
                cardEntry.Value.CardDragged -= OnCardDrag;
                cardEntry.Value.CardBeginDrag -= OnCardBeginDrag;
                cardEntry.Value.CardEndDrag -= OnCardEndDrag;
                
                Destroy(cardEntry.Value.gameObject);
            }
            
            DisplayedCards.Clear();
        }

        public void EnableArrow(bool value)
        {
            _aimArrow.Enabled(value);
        }

        private void OnCardBeginDrag(CardView cardView)
        {
            CardBeginDrag?.Invoke(cardView);
            _dragging = true;
        }
        
        private void OnCardEndDrag(CardView cardView)
        {
            CardEndDrag?.Invoke(cardView);
            _dragging = false;
        }

        private void OnCardDrag(CardView view)
        {
            CardDragged?.Invoke(view);
        }

        private void Update()
        {
            if (!_dragging)
                return;
            var mouse = Input.mousePosition;
            var layer = LayerMask.NameToLayer($"Unit");
            var origin = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 10f));
            var hit = Physics2D.Raycast(origin, Vector2.zero, layer);
            if (hit.transform == null)
            {
                HoveredUnit = null;
                return;
            }
            
            if (!hit.transform.transform.gameObject.TryGetComponent<UnitView>(out var unit))
                return;
            
            HoveredUnit = unit;
        }

        private void OnDestroy()
        {
            foreach (var cardEntry in DisplayedCards)
            {
                cardEntry.Value.CardDragged -= OnCardDrag;
                cardEntry.Value.CardBeginDrag -= OnCardBeginDrag;
                cardEntry.Value.CardEndDrag -= OnCardEndDrag;
                
                Destroy(cardEntry.Value.gameObject);
            }
        }
    }
}