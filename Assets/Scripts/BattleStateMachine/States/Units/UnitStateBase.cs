using Models.Units;
using StateMachine;

namespace BattleStateMachine.States.Impl
{
    public abstract class UnitStateBase : StateBase
    {
        protected UnitStateBase(UnitBattleStateMachine battleStateMachine) : base(battleStateMachine)
        {
            BattleUnit = battleStateMachine.BattleUnit;
        }
        
        protected BattleUnit BattleUnit { get; }
    }
}