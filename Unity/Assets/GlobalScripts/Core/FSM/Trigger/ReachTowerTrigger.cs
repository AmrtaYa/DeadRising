using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class ReachTowerTrigger : FSMTrigger
    {
        public override void init()
        {
            triggerID = FSMTriggerID.ReachTower;
        }

        public override bool triggerHandler(FSMBase fSMBase)
        {
            if (fSMBase.towerTr == null) return false;
            return Vector3.Distance(fSMBase.transform.position, fSMBase.towerTr.position) <= 1.5f;
        }
    }
}