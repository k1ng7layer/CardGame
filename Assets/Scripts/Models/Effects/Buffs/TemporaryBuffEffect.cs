using Settings.Effects;

namespace Models.Effects.Buffs
{
    public class TemporaryBuffEffect : AttributeBuffEffect
    {
        private int _durationTurns;

        public TemporaryBuffEffect(EffectSettings settings) : base(settings)
        { }
        
        public override void Tick()
        {
            _durationTurns--;
            
            if (_durationTurns <= 0)
                Disable();
        }
    }
}