using UnityEngine;

namespace Settings.Battle
{
    [CreateAssetMenu(menuName = "Settings/BattleSettingsBase", fileName = "BattleSettingsBase")]
    public class BattleSettingsBase : ScriptableObject
    {
        [SerializeField] private int _starterCardsNumber;

        public int StarterCardsNumber => _starterCardsNumber;
    }
}