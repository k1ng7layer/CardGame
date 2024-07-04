using System.Collections.Generic;
using Models.Effects.Buffs;
using Models.Units;
using Settings.Effects;

namespace Models.Buff
{
    public class Buff
    {
        private readonly BattleUnit _target;

        public Buff(
            List<AttributeModifierSettings> settingsList, 
            ApplicationType applicationType,
            BattleUnit target)
        {
            _target = target;
            SettingsList = settingsList;
            ApplicationType = applicationType;
        }
        
        public List<AttributeModifierSettings> SettingsList { get; }
        public ApplicationType ApplicationType { get; }

        public void Apply()
        {
            foreach (var settings in SettingsList)
            {
                _target.UnitAttributes[settings.Attribute].Value += settings.Value;
            }
        }
    }
}