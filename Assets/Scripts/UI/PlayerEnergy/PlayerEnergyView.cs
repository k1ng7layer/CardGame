using TMPro;
using UI.Core;
using UnityEngine;

namespace UI.PlayerEnergy
{
    public class PlayerEnergyView : UiView
    {
        [SerializeField] private TextMeshProUGUI _energyText;

        public void DisplayEnergy(float value)
        {
            _energyText.text = $"{value}";
        }
    }
}