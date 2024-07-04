using UI.CardPanel;
using UI.Core;
using UI.EndTurnButton;
using UI.PlayerEnergy;

namespace UI.Windows
{
    public class BattleWindow : UiWindow
    {
        public override void Setup()
        {
            AddController<CardPanelController>();
            AddController<EndTurnButtonController>();
            AddController<PlayerEnergyController>();
        }
    }
}