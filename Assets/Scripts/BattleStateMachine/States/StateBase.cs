using Models.Units;

namespace BattleStateMachine.States
{
    public abstract class StateBase
    {
        public StateBase(BattleStateMachine battleStateMachine)
        {
            BattleStateMachine = battleStateMachine;
        }
        
        public BattleStateMachine BattleStateMachine { get; }
        
        public void Enter()
        {
            OnEnter();
        }

        public void Exit()
        {
            OnExit();
        }

        protected virtual void OnEnter()
        { }
        
        protected virtual void OnExit()
        { }
    }
}