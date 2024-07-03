using System;
using System.Collections.Generic;
using Helpers.Tasks;
using Models.Effects;
using Settings.Effects;

namespace Services.EffectHandler
{
    public class EffectFactory 
    {
        private readonly ICoroutineDispatcher _coroutineDispatcher;
        private readonly Dictionary<Type, Queue<Effect>> _effectHandlersPool = new();

        public EffectFactory(
            EffectSettingsBase effectSettingsBase, 
            ICoroutineDispatcher coroutineDispatcher
        )
        {
            _coroutineDispatcher = coroutineDispatcher;
            foreach (var settings in effectSettingsBase.Effects)
            {
                var queue = new Queue<Effect>();
                var instance = CreateInstance(settings);
                queue.Enqueue(instance);
                _effectHandlersPool.Add(settings.HandlerType.Type, queue);
            }
        }

        public Effect GetHandler(EffectSettings settings)
        {
            return CreateInstance(settings);
        }
        
        public Effect GetHandler<T>(EffectSettings settings)
        {
            return CreateInstance(settings);
        }

        public Effect GetHandlerOfType(EffectSettings effectType)
        {
            Effect effect;
            var type = effectType.HandlerType.Type;

            if (_effectHandlersPool[type].Count > 0)
                effect = _effectHandlersPool[type].Dequeue();
            else
            {
                effect = CreateInstance(effectType);
            }

            return effect;
        }

        private Effect CreateInstance(EffectSettings settings)
        {
            return (Effect)Activator.CreateInstance(settings.HandlerType.Type, new object[]{settings, _coroutineDispatcher});
        }
        
        private T CreateInstance<T>(Type type, EffectSettings settings) where T : Effect
        {
            return (T)Activator.CreateInstance(type, new object[]{settings});
        }
    }
}