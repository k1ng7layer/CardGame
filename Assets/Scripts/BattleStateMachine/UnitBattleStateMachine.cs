using Models.Units;

namespace StateMachine
{
    public class UnitBattleStateMachine : global::BattleStateMachine.BattleStateMachine
    {
        public UnitBattleStateMachine(BattleUnit battleUnit)
        {
            BattleUnit = battleUnit;
        }
        
        public BattleUnit BattleUnit { get; }
    }
}