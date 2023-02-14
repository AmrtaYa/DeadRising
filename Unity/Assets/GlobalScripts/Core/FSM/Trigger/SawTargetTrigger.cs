using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class SawTargetTrigger : FSMTrigger
    {
        public override void init()
        {
            triggerID = FSMTriggerID.SawTarget;
        }

        public override bool triggerHandler(FSMBase fSMBase)
        {
            return fSMBase.targetTr != null;
        }
    }
}