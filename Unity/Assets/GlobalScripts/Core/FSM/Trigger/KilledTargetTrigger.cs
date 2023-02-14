using MiYue;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class KilledTargetTrigger : FSMTrigger
    {
        private Unit targetUnit;
        public override void init()
        {
            triggerID = FSMTriggerID.KilledTarget;
        }

        public override bool triggerHandler(FSMBase fSMBase)
        {
            if (fSMBase.targetTr == null) return true;
            if (targetUnit == null)
                targetUnit = fSMBase.targetTr.GetComponent<Unit>();
            if (targetUnit.Data.HP<=0)
            {
                targetUnit = null;
                return true;
            }

            return false;
        }
    }
}