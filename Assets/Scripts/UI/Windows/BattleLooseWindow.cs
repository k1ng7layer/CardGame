using UI.BattleLoose;
using UI.BattleWin;
using UI.Core;

namespace UI.Windows
{
    public class BattleLooseWindow : UiWindow
    {
        public override void Setup()
        {
            AddController<BattleLooseController>();
        }
    }
}