using MiYue;
using UnityEngine;
namespace FSM
{
    /// <summary>
    /// 没有人在塔上
    /// </summary>
   public class NoOneTowerState : FSMState
   {
         public override void init()
       {
           stateID = FSMStateID.NoOneTower;
       }

         public override void EnterState(FSMBase fSMBase)
         {
             base.EnterState(fSMBase);
             fSMBase.SetAnim(MotionType.Idle);
         }
   }

}