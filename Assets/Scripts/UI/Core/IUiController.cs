using System;

namespace UI.Core
{
    public interface IUiController : IDisposable
    {
        void Initialize();
        void Open();
        void Close();
    }
}