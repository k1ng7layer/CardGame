using System;
using System.Collections.Generic;
using BattleStateMachine.States;

namespace BattleStateMachine
{
    public class BattleStateMachine
    {
        private readonly Dictionary<Type, StateBase> _states = new();
        private StateBase _currentState;

        public void ChangeState<T>() where T : StateBase
        {
            _currentState?.Exit();

            _currentState = _states[typeof(T)];
            _currentState.Enter();
        }

        public void AddState<T>(T state) where T : StateBase
        {
            _states.Add(state.GetType(), state);
        }
    }
}