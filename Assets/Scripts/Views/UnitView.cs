using System.Security.Cryptography;
using Models.Units;
using Settings.Effects;
using UI.Unit;
using UnityEngine;

namespace Views
{
    public class UnitView : MonoBehaviour, IUnitView
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Canvas _unitCanvas;

        protected UnitAttributesController _unitAttributesController;
        
        public virtual void Initialize(BattleUnit unit)
        {
            BattleUnitModel = unit;
            var view = _unitCanvas.GetComponent<UnitAttributesView>();
            _unitAttributesController = new UnitAttributesController(view, unit);
            _unitAttributesController.Initialize();
            BattleUnitModel.PositionChanged += OnPositionChanged;
            BattleUnitModel.AttackPerformed += OnAttackAdded;
            BattleUnitModel.Dead += OnDead;
            
        }
        
        public BattleUnit BattleUnitModel { get; private set; }

        private void OnDestroy()
        {
            BattleUnitModel.Dead -= OnDead;
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
            _animator.SetTrigger(Animator.StringToHash("Attack"));
        }

        protected virtual void OnDead(BattleUnit unit)
        {
            Destroy(gameObject);
        }
        
    }
}