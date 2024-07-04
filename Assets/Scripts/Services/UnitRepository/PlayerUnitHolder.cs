using Models.Units;

namespace Services.UnitRepository
{
    public class PlayerUnitHolder
    {
        public BattleUnit PlayerUnit { get; private set; }

        public void RegisterPlayerUnit(BattleUnit unit)
        {
            PlayerUnit = unit;
        }
    }
}