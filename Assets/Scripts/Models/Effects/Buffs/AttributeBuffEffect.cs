using Models.Units;
using Settings.Effects;

namespace Models.Effects.Buffs
{
    public class AttributeBuffEffect : Effect
    {
        private readonly EffectSettings _effectSettings;
        private BattleUnit _target;
        
        public AttributeBuffEffect(EffectSettings settings) : base(settings)
        { }
        
        public bool Expired { get; private set; }
        
        public override void Apply(BattleUnit user, BattleUnit target)
        {
            _target = target;
            
            foreach (var attributeModifier in _effectSettings.AttributeModifiers)
            {
                if (!target.UnitAttributes.TryGetValue(attributeModifier.Effect, out var attribute))
                    continue;
                
                attribute.AddModifier(attributeModifier);
            }
        }
        
        public void Disable()
        {
            foreach (var attributeModifier in _effectSettings.AttributeModifiers)
            {
                if (!_target.UnitAttributes.TryGetValue(attributeModifier.Effect, out var attribute))
                    continue;
                
                attribute.TryRemoveModifier(attributeModifier);
            }

            Expired = true;
        }
        
        public virtual void Tick()
        { }
    }
}