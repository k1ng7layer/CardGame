using Settings.Cards;
using UnityEngine;

namespace Settings.Units.Player
{
    [CreateAssetMenu(menuName = "Settings/Player/PlayerSettings", fileName = "PlayerSettings")]
    public class PlayerSettings : UnitSettings
    {
        [SerializeField] private Deck _deck;

        public Deck Deck => _deck;
    }
}