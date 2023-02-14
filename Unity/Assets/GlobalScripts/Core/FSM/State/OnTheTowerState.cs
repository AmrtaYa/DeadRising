using GJC.Helper;
using MiYue;
using Unity.VisualScripting;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class OnTheTowerState : FSMState
    {
        private Transform pos;

        public override void init()
        {
            stateID = FSMStateID.OnTheTower;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            if (fSMBase.navAgent != null)
            {
                fSMBase.navAgent.isStopped = true;
            }

            fSMBase.SetAnim(MotionType.Idle);
            var denfence = fSMBase.unit as CommonDenfence;
            denfence.IfOnTheTower = true;
            pos = fSMBase.towerTr.FindTheTfByName("PlayerSetPos");
            fSMBase.transform.SetParent(pos);
            fSMBase.transform.localPosition = Vector3.zero;
        }

        public override void ExitState(FSMBase fSMBase)
        {
            base.ExitState(fSMBase);
            if (fSMBase.navAgent != null)
            {
                fSMBase.navAgent.isStopped = false;
            }

            var denfence = fSMBase.unit as CommonDenfence;
            denfence.IfOnTheTower = false;
            pos = null;
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            if (fSMBase.towerTr == null) return;
            fSMBase.transform.forward = fSMBase.towerTr.forward;
            var denfence = fSMBase.unit as CommonDenfence;
            denfence.IfOnTheTower = true;
            if (pos != null)
                fSMBase.transform.SetParent(pos);
            fSMBase.transform.localPosition = Vector3.zero;
        }
    }
}