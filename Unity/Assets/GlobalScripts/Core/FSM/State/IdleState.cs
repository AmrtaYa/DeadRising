using System.Collections;
using System.Collections.Generic;
using MiYue;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class IdleState : FSMState
    {
        public override void init()
        {
            stateID = FSMStateID.Idle;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            if (fSMBase.navAgent != null)
                fSMBase.navAgent.isStopped = true;
            fSMBase.SetAnim(MotionType.Idle);
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            if (fSMBase.navAgent != null)
                fSMBase.navAgent.isStopped = false;
        }
    }
}