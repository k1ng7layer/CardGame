using UI.CardPanel;
using UI.Core;

namespace UI.Windows
{
    public class BattleWindow : UiWindow
    {
        public override void Setup()
        {
            AddController<CardPanelController>();
        }
    }
}