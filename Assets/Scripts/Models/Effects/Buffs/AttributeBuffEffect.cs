using Models.Units;
using Settings.Effects;

namespace Models.Effects.Buffs
{
    public class AttributeBuffEffect : Effect
    {
        private readonly EffectSettings _effectSettings;
        protected BattleUnit Target;
        
        public AttributeBuffEffect(EffectSettings settings) : base(settings)
        { }
        
        public bool Expired { get; protected set; }
        
        public override void Apply(BattleUnit user, BattleUnit target)
        {
            Target = target;
            
            foreach (var attributeModifier in _effectSettings.AttributeModifiers)
            {
                if (!target.UnitAttributes.TryGetValue(attributeModifier.Effect, out var attribute))
                    continue;
                
                attribute.AddModifier(attributeModifier);
            }

            target.AddBuffEffect(this);
        }
        
        public virtual void Tick()
        { }
    }
}