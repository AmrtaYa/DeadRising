using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class LoseTargetTrigger : FSMTrigger
    {
        public override void init()
        {
            triggerID = FSMTriggerID.LoseTarget;
        }

        public override bool triggerHandler(FSMBase fSMBase)
        {
            return fSMBase.targetTr==null;
        }
    }
}