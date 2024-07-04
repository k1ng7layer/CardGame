using Models.Units;
using Services.Battle;
using Services.UnitRepository;
using UI.Core;

namespace UI.EndTurnButton
{
    public class EndTurnButtonController : UiController<EndTurnButtonView>
    {
        private readonly BattleService _battleService;
        private readonly PlayerUnitHolder _playerUnitHolder;

        public EndTurnButtonController(
            EndTurnButtonView view, 
            BattleService battleService,
            PlayerUnitHolder playerUnitHolder
            
        ) : base(view)
        {
            _battleService = battleService;
            _playerUnitHolder = playerUnitHolder;
        }

        protected override void OnInitialize()
        {
            View.Initialize(EndTurn);
        }

        protected override void OnShow()
        {
            _battleService.CurrentBattle.NextTurnStarted += OnNextTurnStarted;
            _playerUnitHolder.PlayerUnit.InActionStatusChanged += OnActionStatusChanged;
        }

        public override void Dispose()
        {
            _battleService.CurrentBattle.NextTurnStarted -= OnNextTurnStarted;
            _playerUnitHolder.PlayerUnit.InActionStatusChanged -= OnActionStatusChanged;
        }

        private void EndTurn()
        {
            _battleService.CurrentBattle.EndTurn();
        }

        private void OnNextTurnStarted(BattleUnit unit)
        {
            View.SetInteractable(unit == _playerUnitHolder.PlayerUnit);
        }

        private void OnActionStatusChanged(bool inAction)
        {
            View.SetInteractable(!inAction);
        }
    }
}