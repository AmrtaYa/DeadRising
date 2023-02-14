using MiYue;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class ExitTheTowerTrigger : FSMTrigger
    {
        public override void init()
        {
            triggerID = FSMTriggerID.ExitTheTower;
        }

        public override bool triggerHandler(FSMBase fSMBase)
        {
            return fSMBase.towerTr==null;
        }
    }
}