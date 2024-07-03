using Models.Units;
using UI.Unit;
using UnityEngine;

namespace Views
{
    public class UnitView : MonoBehaviour, IUnitView
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Canvas _unitCanvas;

        private UnitAttributesController _unitAttributesController;
        
        public void Initialize(BattleUnit unit)
        {
            BattleUnitModel = unit;
            var view = _unitCanvas.GetComponent<UnitAttributesView>();
            _unitAttributesController = new UnitAttributesController(view, unit);
            _unitAttributesController.Initialize();
            BattleUnitModel.PositionChanged += OnPositionChanged;
            BattleUnitModel.AttackPerformed += OnAttackAdded;
        }
        
        public BattleUnit BattleUnitModel { get; private set; }

        private void OnDestroy()
        {
            BattleUnitModel.PositionChanged -= OnPositionChanged;
            BattleUnitModel.AttackPerformed -= OnAttackAdded;
            _unitAttributesController.Dispose();
        }

        private void OnPositionChanged(Vector3 value)
        {
            transform.position = value;
        }

        private void OnAttackAdded(bool isAttacking)
        {
            _animator.SetTrigger(Animator.StringToHash("Attack1"));
        }
    }
}