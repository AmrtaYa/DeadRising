using MiYue;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
    public class ToHasOneTowerTrigger : FSMTrigger
    {
        public override void init()
        {
            triggerID = FSMTriggerID.ToHasOneTower;
        }

        public override bool triggerHandler(FSMBase fSMBase)
        {
            CommonAtkTower tower = fSMBase.unit as CommonAtkTower;
            if (tower == null) return false;
            return tower.onTheTower;
        }
    }
}