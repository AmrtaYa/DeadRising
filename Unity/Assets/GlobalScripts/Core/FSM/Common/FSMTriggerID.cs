using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 有限状态机触发条件编号
    /// </summary>
    public enum FSMTriggerID
    {
        NoHealth,//没血
        SawTarget,//发现目标
        ReachTarget,//目标进入攻击范围
        LoseTarget,//失去目标
        CompletePatrol,//完成巡逻
        KilledTarget,//杀死目标
        WithoutAttackRange//目标离开攻击范围

        ,
        ReachAttackPoint//到达准备攻击的点了
        ,
        NoReachAttackPoint,
        CanGotoTheTower,
        ExitTheTower,
        ToNoOneTower,
        ToHasOneTower,
        ReachTower
    }
}
