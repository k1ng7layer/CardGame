using System;
using Settings.Cards;
using UI.Core;
using UnityEngine.EventSystems;

namespace UI.CardPanel
{
    public class CardView : UiView, 
        IDragHandler, 
        IBeginDragHandler, 
        IEndDragHandler
    {
        public event Action<CardView> CardBeginDrag;
        public event Action<CardView> CardDragged;
        public event Action<CardView> CardEndDrag;
        public CardSettings AttachedCardSettings { get; private set; }

        public void Initialize(CardSettings cardSettings)
        {
            AttachedCardSettings = cardSettings;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            CardDragged?.Invoke(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            CardBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            CardEndDrag?.Invoke(this);
        }
    }
}