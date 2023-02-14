using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
   public class NoHealthTrigger : FSMTrigger
   {
       public override void init()
       {
           triggerID = FSMTriggerID.NoHealth;
       }

       public override bool triggerHandler(FSMBase fSMBase)
       {
           return fSMBase.unit.Data.HP<=0;
       }
   }
}
