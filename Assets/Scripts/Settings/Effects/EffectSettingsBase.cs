using UnityEngine;

namespace Settings.Effects
{
    [CreateAssetMenu(menuName = "Settings/Effects/EffectSettingsBase", fileName = "EffectSettingsBase")]
    public class EffectSettingsBase : ScriptableObject
    {
        [SerializeField] protected EffectSettings[] _effects;
        
        public EffectSettings[] Effects => _effects;
    }
}