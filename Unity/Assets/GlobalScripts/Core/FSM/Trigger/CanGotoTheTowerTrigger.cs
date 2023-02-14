using MiYue;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class CanGotoTheTowerTrigger : FSMTrigger
    {
        public override void init()
        {
            triggerID = FSMTriggerID.CanGotoTheTower;
        }

        public override bool triggerHandler(FSMBase fSMBase)
        {
            return fSMBase.towerTr!=null;
        }
    }
}