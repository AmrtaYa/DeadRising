using GJC.Helper;
using MiYue;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class NoReachAttackPointTrigger : FSMTrigger
    {
        public override void init()
        {
            triggerID = FSMTriggerID.NoReachAttackPoint;
        }

        public override bool triggerHandler(FSMBase fSMBase)
        {
            if (fSMBase.targetTr == null)
                return false;
            var minTarget = SpawnManager.Instance[SpawnType.DenfenceAttackSpawn].ToArray()
                .GetMin(t => Vector3.Distance(t.position, fSMBase.targetTr.position));
            return Vector3.Distance(minTarget.position.XyzToX0z(), fSMBase.transform.position.XyzToX0z()) > 1F;

        }
    }
}