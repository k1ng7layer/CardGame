using System;
using System.Collections.Generic;
using Models.Units;
using Services.Battle.StateMachine.States;

namespace Services.Battle.StateMachine
{
    public class UnitStateMachine
    {
        private readonly Dictionary<Type, StateBase> _states = new();
        private StateBase _currentState;
        
        public UnitStateMachine(BattleUnit battleUnit)
        {
            BattleUnit = battleUnit;
        }
        
        public BattleUnit BattleUnit { get; }

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