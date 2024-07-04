using System.Collections.Generic;
using BattleStateMachine.States.Impl;
using Models.Buff;
using Settings.Effects;
using StateMachine;

namespace BattleStateMachine.States.Units
{
    public class OnTurnFinishState : UnitStateBase
    {
        public OnTurnFinishState(UnitBattleStateMachine battleStateMachine) : base(battleStateMachine)
        {
        }

        protected override void OnEnter()
        {
            var expired = new List<Buff>();
            
            foreach (var buff in BattleUnit.Buffs)
            {
                if (buff.ApplicationType != ApplicationType.TurnEnd)
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