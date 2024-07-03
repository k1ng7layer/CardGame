using System;
using Models.Units;
using Settings.Effects;
using UI.Core;

namespace UI.Unit
{
    public class UnitAttributesController : UiController<UnitAttributesView>, IDisposable
    {
        private readonly BattleUnit _unit;

        public UnitAttributesController(UnitAttributesView view, BattleUnit unit) : base(view)
        {
            _unit = unit;
        }

        protected override void OnInitialize()
        {
            var health = _unit.UnitAttributes[EAttributeType.Health];
            var block = _unit.UnitAttributes[EAttributeType.Block];
            
            health.ValueChanged += OnHealthChanged;
            block.ValueChanged += OnBlockChanged;
            
            OnHealthChanged(health.Value);
            OnBlockChanged(block.Value);
        }
        
        public void Dispose()
        {
            _unit.UnitAttributes[EAttributeType.Health].ValueChanged -= OnHealthChanged;
            _unit.UnitAttributes[EAttributeType.Block].ValueChanged -= OnBlockChanged;
        }

        private void OnHealthChanged(float newValue)
        {
            var maxHealth = _unit.UnitAttributes[EAttributeType.Health].MaxValue;
            View.SetHealth(newValue, maxHealth);
        }

        private void OnBlockChanged(float block)
        {
            View.SetBlock(block);
        }
    }
}