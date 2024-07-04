using System;
using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BattleWin
{
    public class BattleWinView : UiView
    {
        [SerializeField] private Button _nextButton;
        
        private Action _onNextBattle;

        private void Awake()
        {
            _nextButton.onClick.AddListener(Restart);
        }

        private void OnDestroy()
        {
            _nextButton.onClick.RemoveListener(Restart);
        }

        public void Initialize(Action onNext)
        {
            _onNextBattle = onNext;
        }

        private void Restart()
        {
            _onNextBattle?.Invoke();
        }
    }
}