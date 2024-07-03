using Helpers.Tasks;
using Settings.Effects;

namespace Models.Effects.Buffs
{
    public class TemporaryBuffEffect : AttributeBuffEffect
    {
        private readonly EffectSettings _settings;
        private int _durationTurns;

        public TemporaryBuffEffect(EffectSettings settings, 
            ICoroutineDispatcher coroutineDispatcher) : base(settings, coroutineDispatcher)
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
                if (!Target.UnitAttributes.TryGetValue(attributeModifier.Attribute, out var attribute))
                    continue;
                
                attribute.TryRemoveModifier(attributeModifier);
            }

            Expired = true;
        }
    }
}