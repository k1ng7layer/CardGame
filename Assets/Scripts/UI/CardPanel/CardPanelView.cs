using System;
using System.Collections.Generic;
using Settings.Cards;
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
        [SerializeField] private List<CardView> _cardViews;
        
        public event Action<CardView> CardBeginDrag;
        public event Action<CardView> CardDragged;
        public event Action<CardView> CardEndDrag;
        public event Action<UnitView> PointerOnUnitEnter;
        public event Action<UnitView> PointerOnUnitExit;

        public UnitView HoveredUnit { get; private set; }
        private bool _dragging;
        
        public void SetCardUseAbility(bool value)
        {
            _aimArrow.SetHeadColor(value ? Color.green : Color.red);
        }

        public void DisplayCard(CardSettings cardSettings)
        {
            var cardView = Instantiate(_cardViewPrefab, _cardListRoot);
            cardView.Initialize(cardSettings);
            cardView.CardDragged += OnCardDrag;
            cardView.CardBeginDrag += OnCardBeginDrag;
            cardView.CardDragged += OnCardEndDrag;
            
            _cardViews.Add(cardView);
        }

        public void EnableArrow(bool value)
        {
            _aimArrow.Enabled(value);
        }

        private void OnCardBeginDrag(CardView cardView)
        {
            _dragging = true;
        }
        
        private void OnCardEndDrag(CardView cardView)
        {
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

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var layer = LayerMask.NameToLayer($"Unit");
            
            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, layer))
                return;
            
            if (!hit.transform.transform.gameObject.TryGetComponent<UnitView>(out var unit))
                return;

            HoveredUnit = unit;
        }

        private void OnDestroy()
        {
            foreach (var cardView in _cardViews)
            {
                cardView.CardDragged -= OnCardDrag;
                cardView.CardBeginDrag -= OnCardBeginDrag;
                cardView.CardEndDrag -= OnCardEndDrag;
            }
        }
    }
}