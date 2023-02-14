using System.Linq;
using GJC.Helper;
using MiYue;
using UnityEngine;
namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
   public class PrePareAttackState : FSMState
   {
         public override void init()
       {
           stateID = FSMStateID.PrePareAttack;
       }

         public override void EnterState(FSMBase fSMBase)
         {
             base.EnterState(fSMBase);
             fSMBase.navAgent.isStopped = false;
         }

         public override void ActionState(FSMBase fSMBase)
         {
             base.ActionState(fSMBase);
             if(fSMBase.targetTr==null)
                 return;
             var minTarget = SpawnManager.Instance.RandomGetMinPoint(fSMBase.targetTr.position, SpawnType.DenfenceAttackSpawn);
             fSMBase.Move(minTarget.position,0,fSMBase.unit.Data.Speed);
             fSMBase.SetAnim(MotionType.Run);
         }
   }

}