using MiYue;
using UnityEngine;
namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
   public class FindTheTowerState : FSMState
   {
         public override void init()
       {
           stateID = FSMStateID.FindTheTower;
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
             fSMBase.Move(fSMBase.towerTr.position,0,fSMBase.unit.Data.Speed);
         }

      
   }

}