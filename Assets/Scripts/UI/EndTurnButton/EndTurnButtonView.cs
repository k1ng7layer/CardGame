using System;
using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.EndTurnButton
{
    public class EndTurnButtonView : UiView
    {
        [SerializeField] private Button _button;
        private Action _onEndTurn;

        private void Awake()
        {
            _button.onClick.AddListener(EndTurn);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(EndTurn);
        }

        public void Initialize(Action onEndTurn)
        {
            _onEndTurn = onEndTurn;
        }

        public void SetInteractable(bool value)
        {
            _button.interactable = value;
        }

        private void EndTurn()
        {
            _onEndTurn?.Invoke();
        }
    }
}