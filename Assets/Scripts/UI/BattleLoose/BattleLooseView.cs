using System;
using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BattleLoose
{
    public class BattleLooseView : UiView
    {
        [SerializeField] private Button _restartButton;
        
        private Action _onRestart;

        private void Awake()
        {
            _restartButton.onClick.AddListener(Restart);
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveListener(Restart);
        }

        public void Initialize(Action onRestart)
        {
            _onRestart = onRestart;
        }

        private void Restart()
        {
            _onRestart?.Invoke();
        }
    }
}