using TMPro;
using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Unit
{
    public class UnitAttributesView : UiView
    {
        [SerializeField] private TextMeshProUGUI _blockText;
        [SerializeField] private TextMeshProUGUI _healthText;
        [SerializeField] private Image _healthBar;
        [SerializeField] private TextMeshProUGUI _currentAction;

        public void SetHealth(float health, float maxHealth)
        {
            _healthText.text = $"{health}";
            _healthBar.fillAmount = health == 0 ? 0 : health / maxHealth;
        }

        public void SetBlock(float block)
        {
            _blockText.text = $"{block}";
        }

        public void SetCurrentActionInfo(string info)
        {
            _currentAction.text = info;
        }
    }
}