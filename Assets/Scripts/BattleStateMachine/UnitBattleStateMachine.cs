using Models.Units;

namespace StateMachine
{
    public class UnitBattleStateMachine : BattleStateMachine.BattleStateMachine
    {
        public UnitBattleStateMachine(BattleUnit battleUnit)
        {
            BattleUnit = battleUnit;
        }
        
        public BattleUnit BattleUnit { get; }
    }
}