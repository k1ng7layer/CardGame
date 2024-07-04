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
            _effectSettings = settings;
            _coroutineDispatcher = coroutineDispatcher;
        }
        
        public bool Expired { get; protected set; }
        
        public override void Apply(BattleUnit user, BattleUnit target)
        {
            Target = user;

            if (_effectSettings.ModifiersApplicationType == ApplicationType.Instant)
            {
                foreach (var attributeModifier in _effectSettings.AttributeModifiers)
                {
                    if (!Target.UnitAttributes.TryGetValue(attributeModifier.Attribute, out var attribute))
                        continue;

                    attribute.Value += attributeModifier.Value;
                }
            }
            else
            {
                var buff = new Buff.Buff(
                    _effectSettings.AttributeModifiers, 
                    _effectSettings.ModifiersApplicationType,
                    target);
                
                Target.AddBuffEffect(buff);
            }
            
            Finish();
        }
        
        public virtual void Tick()
        { }
    }
}