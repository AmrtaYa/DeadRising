using MiYue;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class PursuitState : FSMState
    {
        public override void init()
        {
            stateID = FSMStateID.Pursuit;
        }

        public override void EnterState(FSMBase fSMBase)
        {
            base.EnterState(fSMBase);
            if (fSMBase.navAgent != null)
                fSMBase.navAgent.isStopped = false;
            fSMBase.SetAnim(MotionType.Run);
        }

        public override void ActionState(FSMBase fSMBase)
        {
            base.ActionState(fSMBase);
            fSMBase.Move(fSMBase.targetTr.position, fSMBase.unit.Data.AttackRange - 0.1F,
                fSMBase.unit.Data.Speed); //减去1.0是怕攻击不到
        }
    }
}