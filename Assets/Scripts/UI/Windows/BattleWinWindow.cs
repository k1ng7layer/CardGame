﻿using UI.BattleWin;
using UI.Core;

namespace UI.Windows
{
    public class BattleWinWindow : UiWindow
    {
        public override void Setup()
        {
            AddController<BattleWinController>();
        }
    }
}