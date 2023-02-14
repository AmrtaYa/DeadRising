using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 有限状态机触发
    /// </summary>
    public abstract class FSMTrigger 
    {
        public FSMTriggerID triggerID;
        public FSMTrigger()
        {
            init();
        }
        public abstract void init();
        public abstract bool triggerHandler(FSMBase fSMBase);//触发条件
    }
}
