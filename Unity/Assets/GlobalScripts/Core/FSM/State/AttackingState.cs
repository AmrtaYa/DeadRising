using GJC.Helper;
using MiYue;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class AttackingState : FSMState
    {
        public override void init()
        {
            stateID = FSMStateID.Attacking;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            fSMBase.SetAnim(MotionType.Attack);
            if (fSMBase.navAgent != null)
                fSMBase.navAgent.isStopped = true;
            fSMBase.ChangeNav(false);
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            fSMBase.transform.forward = fSMBase.targetTr.position.XyzToX0z() - fSMBase.transform.position.XyzToX0z();
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            fSMBase.ChangeNav(true);
        }
    }
}