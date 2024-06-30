using System.Collections.Generic;
using Services.Battle.StateMachine;
using Services.Battle.StateMachine.States;
using Services.Battle.StateMachine.States.Impl;

namespace Factories.StateMachineBuilder.Impl
{
    public class PlayerStateMachineBuilder : BattleUnitStateMachineBuilder
    {
        protected override void ComposeStates(List<StateBase> stateList, UnitStateMachine stateMachine)
        {
            stateMachine.AddState(new OnTurnFinishState(stateMachine));
            stateMachine.AddState(new OnTurnStartState(stateMachine));
        }
    }
}