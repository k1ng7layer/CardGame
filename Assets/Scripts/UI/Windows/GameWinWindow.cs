using UI.Core;
using UI.GameWin;

namespace UI.Windows
{
    public class GameWinWindow : UiWindow
    {
        public override void Setup()
        {
            AddController<GameWinController>();
        }
    }
}