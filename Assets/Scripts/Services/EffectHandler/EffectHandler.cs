using System;
using System.Collections.Generic;
using Models.Effects;
using Settings.Effects;

namespace Services.EffectHandler
{
    public class EffectHandler 
    {
        private readonly Dictionary<Type, Effect> _effects = new();
        
        public void AddHandler<T>(EffectSettings settings) where T : Effect, new ()
        {
            var handler = (T)Activator.CreateInstance(typeof(T), new object[]{settings});
            
            _effects.Add(typeof(T), handler);
        }

        public Effect GetHandler<T>() where T : Effect
        {
            return _effects[typeof(T)];
        }
    }
}