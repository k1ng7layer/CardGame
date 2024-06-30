using Models.Units;
using Settings.Effects;

namespace Models.Effects.Buffs
{
    public class AttributeBuffEffect : Effect
    {
        private readonly EffectSettings _effectSettings;

        public AttributeBuffEffect(BattleUnit target,
            ApplicationType applicationType, EffectSettings effectSettings) : base(target, applicationType)
        {
            _effectSettings = effectSettings;
        }
        
        public bool Expired { get; private set; }
        
        public override void Apply()
        {
            foreach (var attributeModifier in _effectSettings.AttributeModifiers)
            {
                if (!Target.UnitAttributes.TryGetValue(attributeModifier.Effect, out var attribute))
                    continue;
                
                attribute.AddModifier(attributeModifier);
            }
        }
        
        public void Disable()
        {
            foreach (var attributeModifier in _effectSettings.AttributeModifiers)
            {
                if (!Target.UnitAttributes.TryGetValue(attributeModifier.Effect, out var attribute))
                    continue;
                
                attribute.TryRemoveModifier(attributeModifier);
            }

            Expired = true;
        }
        
        public virtual void Tick()
        { }
        
    }
}