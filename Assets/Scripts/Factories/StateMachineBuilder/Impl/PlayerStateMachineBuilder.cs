using System.Collections.Generic;
using BattleStateMachine.States;
using BattleStateMachine.States.Impl;
using BattleStateMachine.States.Units;
using StateMachine;

namespace Factories.StateMachineBuilder.Impl
{
    public class PlayerStateMachineBuilder : UnitStateMachineBuilder
    {
        protected override void ComposeStates(List<StateBase> stateList, UnitBattleStateMachine battleStateMachine)
        {
            battleStateMachine.AddState(new OnTurnFinishState(battleStateMachine));
            battleStateMachine.AddState(new PlayerStartTurnState(battleStateMachine));
        }
    }
}