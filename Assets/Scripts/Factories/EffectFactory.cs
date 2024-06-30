using Models.Effects.Buffs;
using Models.Units;
using Settings.Effects;

namespace Factories
{
    public class EffectFactory
    {
        public AttributeBuffEffect CreateBuffEffect(
            BattleUnit target, 
            LifetimeType lifetimeType, 
            EffectSettings effectSettings,
            int duration = 0
        )
        {
            return lifetimeType switch
            {
                LifetimeType.Permanent => new AttributeBuffEffect(target, effectSettings.ApplicationType, 
                    effectSettings),
                LifetimeType.Temporary => new TemporaryBuffEffect(target, 
                    effectSettings.ApplicationType, effectSettings, duration),
            };
        }
    }
}