using System.Collections;
using System.Collections.Generic;
using MiYue;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class DeadState : FSMState
    {
        public override void init()
        {
            stateID = FSMStateID.Dead;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            fSMBase.SetAnim(MotionType.Dead);
            if (fSMBase.navAgent != null)
                fSMBase.navAgent.isStopped = true;
        }
    }
}