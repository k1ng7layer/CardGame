﻿namespace UI.Core
{
    public abstract class UiController<T> : IUiController where T : UiView
    {
        public UiController(T view)
        {
            View = view;
        } 
        
        protected T View { get; }

        public void Open()
       {
           View.Show();
           OnShow();
       }

        public void Close()
       {
           OnHide();
           View.Hide();
       }

        protected virtual void OnShow(){}
        protected virtual void OnHide(){}
    }
}