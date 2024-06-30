using System.Collections.Generic;
using UnityEngine;

namespace Settings.Effects
{
    [CreateAssetMenu(menuName = "Settings/Effects/Effect", fileName = "NewEffect")]
    public class EffectSettings : ScriptableObject
    {
        [Header("Lifetime settings")]
        public LifetimeType LifetimeType;
        public int LifeTimeTurns;

        [Space]
        [Header("Application settings")] 
        public ApplicationType ApplicationType;
        
        [Space]
        [Header("Modifiers settings")]
        public List<AttributeModifierSettings> AttributeModifiers;
    }
}