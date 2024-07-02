using System;
using Settings.Cards;
using TMPro;
using UI.Core;
using UnityEngine;
using UnityEngine.EventSystems;

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
        public event Action<CardView> CardBeginDrag;
        public event Action<CardView> CardDragged;
        public event Action<CardView> CardEndDrag;
        public CardSettings AttachedCardSettings { get; private set; }

        public void Initialize(CardSettings cardSettings)
        {
            AttachedCardSettings = cardSettings;
            _title.text = cardSettings.Title;
            _description.text = cardSettings.Description;
            _cost.text = $"{cardSettings.Cost}";
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            CardDragged?.Invoke(this);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log($"OnBeginDrag");
            CardBeginDrag?.Invoke(this);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log($"OnEndDrag");

            CardEndDrag?.Invoke(this);
        }
    }
}