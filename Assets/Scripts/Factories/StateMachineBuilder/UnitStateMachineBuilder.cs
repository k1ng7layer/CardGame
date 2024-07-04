using System.Collections.Generic;
using BattleStateMachine.States;
using Models.Units;
using StateMachine;

namespace Factories.StateMachineBuilder
{
    public abstract class UnitStateMachineBuilder
    {
        public UnitBattleStateMachine Build(BattleUnit unit)
        {
            var stateMachine = new UnitBattleStateMachine(unit);
            var states = new List<StateBase>();
            ComposeStates(states, stateMachine);

            return stateMachine;
        }

        protected abstract void ComposeStates(List<StateBase> stateList, UnitBattleStateMachine battleStateMachine);
    }
}