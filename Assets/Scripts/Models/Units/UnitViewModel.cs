using System;

namespace Models.Units
{
    public class UnitViewModel 
    {
        public UnitAttribute Health { get; private set; }
        public UnitAttribute Energy { get; private set; }
        public UnitAttribute Armor { get; private set; }

        public Action<float> HealthChanged;
        public Action<float> EnergyChanged;
        public Action<float> ArmorChanged;

        public void ChangeHealth(float value)
        {
            Health.Value = value;
            HealthChanged?.Invoke(Health.Value);
        }
        
        public void ChangeEnergy(float value)
        {
            Energy.Value = value;
            EnergyChanged?.Invoke(Energy.Value);
        }
        
        public void ChangeArmor(float value)
        {
            Armor.Value = value;
            ArmorChanged?.Invoke(Armor.Value);
        }
    }
}