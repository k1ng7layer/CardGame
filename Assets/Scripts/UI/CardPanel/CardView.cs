using System;
using System.Text;
using Models.Cards;
using Settings.Cards;
using Settings.Effects;
using TMPro;
using UI.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
            SetDescription(card.Settings);
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

        private void SetDescription(CardSettings settings)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var attributeModifier in settings.Effect.AttributeModifiers)
            {
                var sign = GetSign(attributeModifier.Value);
                builder.AppendLine($"{sign} {attributeModifier.Value} {attributeModifier.Attribute}");
            }
            
            if (settings.Effect.Damage > 0)
                builder.AppendLine($"Deals {settings.Effect.Damage} damage");
            
            builder.AppendLine($"{GetApplicationType(settings.Effect.ModifiersApplicationType)}");
            
            _description.text = $"{builder}";
        }

        private string GetSign(float value)
        {
            return value >= 0 ? "+" : "-";
        }

        private string GetApplicationType(ApplicationType applicationType)
        {
            return applicationType switch
            {
                ApplicationType.Instant => "at the time of use",
                ApplicationType.TurnStart => "at the beginning of th next turn",
                ApplicationType.TurnEnd => "at the end of the current turn",
            };
        }
    }
}