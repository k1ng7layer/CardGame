using System.Collections.Generic;
using Helpers.SerializableType;
using Models.Effects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings.Effects
{
    [CreateAssetMenu(menuName = "Settings/Effects/Effect", fileName = "NewEffect")]
    public class EffectSettings : ScriptableObject
    {
        [TypeFilter(typeof(Effect))]
        public SerializableType HandlerType;
        
        [Space]
        [Header("Modifiers settings")]
        public ApplicationType ModifiersApplicationType;
        public List<AttributeModifierSettings> AttributeModifiers;

        [Space]
        [TextArea(5, 5)]
        public string Description;
        
        [Space] 
        public float Damage;
    }
}