using Models.Units;
using Settings.Effects;

namespace Models.Effects.Buffs
{
    public class TemporaryBuffEffect : AttributeBuffEffect
    {
        private int _durationTurns;
        
        public TemporaryBuffEffect(BattleUnit target, 
            ApplicationType applicationType, 
            EffectSettings effectSettings,
            int durationTurns
        ) : base(target, applicationType, effectSettings)
        {
            _durationTurns = durationTurns;
        }

        public override void Tick()
        {
            _durationTurns--;
            
            if (_durationTurns <= 0)
                Disable();
        }
    }
}