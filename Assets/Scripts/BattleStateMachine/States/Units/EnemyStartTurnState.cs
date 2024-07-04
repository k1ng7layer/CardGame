using BattleStateMachine.States.Impl;
using StateMachine;

namespace BattleStateMachine.States.Units
{
    public class EnemyStartTurnState : UnitStateBase
    {
        public EnemyStartTurnState(UnitBattleStateMachine battleStateMachine) : base(battleStateMachine)
        {
            
        }

        protected override void OnEnter()
        {
            base.OnEnter();
            
            BattleStateMachine.ChangeState<EnemyChooseActionState>();
        }
    }
}