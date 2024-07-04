using System;
using System.Collections.Generic;
using UI.Core.Signals;

namespace UI.Core
{
    public class UiManager : IDisposable
    {
        private readonly Dictionary<Type, IUiController> _controllers = new();
        private readonly Dictionary<Type, IUiWindow> _windows = new();
        private IUiWindow _currentWindow;

        public UiManager(
            List<IUiWindow> uiWindows, 
            List<IUiController> controllers
        )
        {
            foreach (var controller in controllers)
            {
                _controllers.Add(controller.GetType(), controller);
                controller.Close();
            }
            
            foreach (var window in uiWindows)
            {
                window.Setup();
                
                _windows.Add(window.GetType(), window);
            }
        }

        public void Dispose()
        {
            Supyrb.Signals.Get<SignalOpenWindow>().RemoveListener(OpenNewWindow);
            Supyrb.Signals.Get<SignalCloseWindow>().RemoveListener(CloseLastWindow);

            foreach (var controller in _controllers)
            {
                controller.Value.Dispose();
            }
            
            _controllers.Clear();
            _windows.Clear();
        }

        public void Initialize()
        {
            Supyrb.Signals.Get<SignalOpenWindow>().AddListener(OpenNewWindow);
            Supyrb.Signals.Get<SignalCloseWindow>().AddListener(CloseLastWindow);

            foreach (var controllerEntry in _controllers)
            {
                controllerEntry.Value.Initialize();
            }
        }

        private void OpenNewWindow(Type windowType)
        {
            if (_currentWindow != null)
                CloseWindow(_currentWindow);

            var window = _windows[windowType];
            OpenWindow(window);
        }

        private void OpenWindow(IUiWindow uiWindow)
        {
            _currentWindow = uiWindow;
            
            foreach (var controllerType in uiWindow.Controllers)
            {
                var controller = _controllers[controllerType];
                controller.Open();
            }
        }

        private void CloseWindow(IUiWindow window)
        {
            foreach (var controllerType in window.Controllers)
            {
                var controller = _controllers[controllerType];
                controller.Close();
            }
        }

        private void CloseLastWindow()
        {
            if (_currentWindow != null)
                CloseWindow(_currentWindow);
        }
    }
}