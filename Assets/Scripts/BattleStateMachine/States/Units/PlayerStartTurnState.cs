using System.Collections.Generic;
using BattleStateMachine.States.Impl;
using Models.Buff;
using Settings.Effects;
using StateMachine;

namespace BattleStateMachine.States.Units
{
    public class PlayerStartTurnState : UnitStateBase
    {
        public PlayerStartTurnState(UnitBattleStateMachine battleStateMachine) : base(battleStateMachine)
        {
            
        }

        protected override void OnEnter()
        {
            var expired = new List<Buff>();
            
            var energy = BattleUnit.UnitAttributes[EAttributeType.Energy];
            energy.Value = energy.BaseValue;
            
            foreach (var buff in BattleUnit.Buffs)
            {
                if (buff.ApplicationType != ApplicationType.TurnStart)
                    continue;
                
                buff.Apply();
                expired.Add(buff);
            }
            
            foreach (var buff in expired)
            {
                BattleUnit.RemoveBuffEffect(buff);
            }
        }
    }
}