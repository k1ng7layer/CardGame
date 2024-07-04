using System;
using Models.Cards;
using Settings.Cards;
using TMPro;
using UI.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Card = Models.Cards.Card;

namespace UI.CardPanel
{
    public class CardView : UiView, 
        IDragHandler, 
        IBeginDragHandler, 
        IEndDragHandler
    {
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private TextMeshProUGUI _cost;
        [SerializeField] private Image _bg;
        [SerializeField] private Color _onDragColor;
        
        private Color _normalColor;
        private bool _interactable;
        
        public event Action<CardView> CardBeginDrag;
        public event Action<CardView> CardDragged;
        public event Action<CardView> CardEndDrag;
        public Card AttachedCard { get; private set; }

        public void Initialize(Card card)
        {
            AttachedCard = card;
            _title.text = card.Settings.Title;
            _description.text = card.Settings.Description;
            _cost.text = $"{card.Cost}";
            _normalColor = _bg.color;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (!_interactable)
                return;
            
            CardDragged?.Invoke(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_interactable)
                return;
            
            _bg.color = _onDragColor;
            CardBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_interactable)
                return;
            
            _bg.color = _normalColor;
            CardEndDrag?.Invoke(this);
        }

        public void SetInteractable(bool value)
        {
            _interactable = value;
        }
    }
}