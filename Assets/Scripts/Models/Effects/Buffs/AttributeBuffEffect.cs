using Helpers.Tasks;
using Models.Units;
using Settings.Effects;

namespace Models.Effects.Buffs
{
    public class AttributeBuffEffect : Effect
    {
        private readonly ICoroutineDispatcher _coroutineDispatcher;
        private readonly EffectSettings _effectSettings;
        protected BattleUnit Target;
        
        public AttributeBuffEffect(EffectSettings settings, 
            ICoroutineDispatcher coroutineDispatcher) 
            : base(settings, coroutineDispatcher)
        {
            _coroutineDispatcher = coroutineDispatcher;
        }
        
        public bool Expired { get; protected set; }
        
        public override void Apply(BattleUnit user, BattleUnit target)
        {
            Target = target;
            
            foreach (var attributeModifier in _effectSettings.AttributeModifiers)
            {
                if (!target.UnitAttributes.TryGetValue(attributeModifier.Attribute, out var attribute))
                    continue;
                
                attribute.AddModifier(attributeModifier);
            }

            target.AddBuffEffect(this);
        }
        
        public virtual void Tick()
        { }
    }
}