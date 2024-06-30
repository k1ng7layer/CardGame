using Models.Units;

namespace Services.Battle.StateMachine.States
{
    public abstract class StateBase
    {
        public StateBase(UnitStateMachine unitStateMachine)
        {
            UnitStateMachine = unitStateMachine;
            BattleUnit = unitStateMachine.BattleUnit;
        }
        
        public UnitStateMachine UnitStateMachine { get; }
        
        protected BattleUnit BattleUnit { get; }
        
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