using UnityEngine;

namespace Settings.Battle
{
    public class BattleSettingsBase : ScriptableObject
    {
        [SerializeField] private BattleSettings _gameBattles;

        public BattleSettings BattleSettings => _gameBattles;
    }
}