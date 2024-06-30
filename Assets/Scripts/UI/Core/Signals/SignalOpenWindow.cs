using System;
using Supyrb;

namespace UI.Core.Signals
{
    public class SignalOpenWindow : Signal<SignalOpenWindow>
    {
        public readonly Type WindowType;
        
        public SignalOpenWindow(Type viewType)
        {
            WindowType = viewType;
        }
    }
}