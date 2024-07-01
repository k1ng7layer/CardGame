using UnityEngine;

namespace Settings.Effects
{
    public abstract class EffectSettingsBase : ScriptableObject
    {
        [SerializeField] protected EffectSettings[] _effects;
        
        public EffectSettings[] Effects => _effects;
    }
}