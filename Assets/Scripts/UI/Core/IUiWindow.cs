using System;
using System.Collections.Generic;

namespace UI.Core
{
    public interface IUiWindow
    {
        List<Type> Controllers { get; }
        void Setup();
    }
}