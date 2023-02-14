using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class WithoutAttackRangeTrigger : FSMTrigger
    {
        public override void init()
        {
            triggerID = FSMTriggerID.WithoutAttackRange;
        }

        public override bool triggerHandler(FSMBase fSMBase)
        {
            if (fSMBase.targetTr == null) return false;
            return
                Vector3.Distance
                (new Vector3(fSMBase.transform.position.x, 0, fSMBase.transform.position.z),
                    new Vector3(fSMBase.targetTr.transform.position.x, 0, fSMBase.targetTr.transform.position.z)) >=
                fSMBase.unit.Data.AttackRange;
        }
    }
}