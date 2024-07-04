using Services.UnitRepository;
using Settings.Effects;
using UI.Core;

namespace UI.PlayerEnergy
{
    public class PlayerEnergyController : UiController<PlayerEnergyView>
    {
        private readonly PlayerUnitHolder _playerUnitHolder;

        public PlayerEnergyController(
            PlayerEnergyView view, 
            PlayerUnitHolder playerUnitHolder
        ) : base(view)
        {
            _playerUnitHolder = playerUnitHolder;
        }

        protected override void OnShow()
        {
            var energy =  _playerUnitHolder.PlayerUnit.UnitAttributes[EAttributeType.Energy];
            energy.ValueChanged += OnEnergyUpdated;
            
            View.DisplayEnergy(energy.Value);
        }

        private void OnEnergyUpdated(float value)
        {
            View.DisplayEnergy(value);
        }

        public override void Dispose()
        {
            _playerUnitHolder.PlayerUnit.UnitAttributes[EAttributeType.Energy].ValueChanged -= OnEnergyUpdated;
        }
    }
}