using System.Collections;
using System.Collections.Generic;
using MiYue;
using Unity.VisualScripting;
using UnityEngine;
namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
   public class ToNoOneTowerTrigger : FSMTrigger
   {
       public override void init()
       {
           triggerID = FSMTriggerID.ToNoOneTower;
       }

       public override bool triggerHandler(FSMBase fSMBase)
       {
          CommonAtkTower tower = fSMBase.unit as CommonAtkTower;
          if (tower == null) return false;
          return !tower.onTheTower;
       }
   }
}
