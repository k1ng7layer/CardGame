using System.Collections.Generic;
using Models.Units;
using Services.Battle.StateMachine;
using Services.Battle.StateMachine.States;

namespace Factories.StateMachineBuilder
{
    public abstract class BattleUnitStateMachineBuilder
    {
        public UnitStateMachine Build(BattleUnit unit)
        {
            var stateMachine = new UnitStateMachine(unit);
            var states = new List<StateBase>();
            ComposeStates(states, stateMachine);

            return stateMachine;
        }

        protected abstract void ComposeStates(List<StateBase> stateList, UnitStateMachine stateMachine);
    }
}