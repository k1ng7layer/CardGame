﻿using System.Collections.Generic;
using Models.Effects.Buffs;
using Settings.Effects;

namespace Services.Battle.StateMachine.States.Impl
{
    public class OnTurnFinishState : StateBase
    {
        public OnTurnFinishState(UnitStateMachine unitStateMachine) : base(unitStateMachine)
        {
        }

        protected override void OnEnter()
        {
            var expired = new List<AttributeBuffEffect>();
            
            foreach (var effect in BattleUnit.Effects)
            {
                if (effect.ApplicationType != ApplicationType.TurnEnd)
                    continue;
                
                effect.Tick();
                
                if (effect.Expired)
                    expired.Add(effect);
            }

            foreach (var effect in expired)
            {
                BattleUnit.RemoveBuffEffect(effect);
            }
        }
    }
}