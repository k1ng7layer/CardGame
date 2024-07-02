using Settings.Effects;

namespace Models.Effects.Buffs
{
    public class TemporaryBuffEffect : AttributeBuffEffect
    {
        private readonly EffectSettings _settings;
        private int _durationTurns;

        public TemporaryBuffEffect(EffectSettings settings) : base(settings)
        {
            _settings = settings;
        }
        
        
        public override void Tick()
        {
            _durationTurns--;
            
            if (_durationTurns <= 0)
                Disable();
        }
        
        public void Disable()
        {
            foreach (var attributeModifier in _settings.AttributeModifiers)
            {
                if (!Target.UnitAttributes.TryGetValue(attributeModifier.Effect, out var attribute))
                    continue;
                
                attribute.TryRemoveModifier(attributeModifier);
            }

            Expired = true;
        }
    }
}