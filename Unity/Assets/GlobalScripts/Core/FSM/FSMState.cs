using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// 有限状态机中的状态
    /// </summary>
    public abstract class FSMState
    {
        public FSMStateID stateID;
        private List<FSMTrigger> triggers;
        private Dictionary<FSMTriggerID, FSMStateID> map;
        public FSMState()
        {
            map = new Dictionary<FSMTriggerID, FSMStateID>();
            triggers = new List<FSMTrigger>();
            init();
        }
        /// <summary>
        /// 每帧检测，是否切换状态
        /// </summary>
        public void Reason(FSMBase fsmBase)
        {
            if (triggers.Count == 0) return;
            for (int i = 0; i < triggers.Count; i++)
            {
                if (triggers[i].triggerHandler(fsmBase))
                {
                    FSMStateID stateID = map[triggers[i].triggerID];
                    fsmBase.ChangeState(stateID);
                }
            }
        }

        public abstract void init();
        public void AddMap(FSMTriggerID fsmTriggerID, FSMStateID fsmStateID)//给这个状态加上   触发某个状态后，转换到那个状态    例如  idle状态，idle.addMap(noHealth,dead)   idle状态中，就会有没有血，转换到dead状态
        {
            map.Add(fsmTriggerID, fsmStateID);//创建这个trigger的投影状态
            CreateTrigger(fsmTriggerID);//创造触发条件的这个类，并且加入到这个状态的触发条件中。
        }

        private void CreateTrigger(FSMTriggerID fsmTriggerID)
        {
            Type type = Type.GetType("FSM." + fsmTriggerID.ToString() + "Trigger");//命名规范：命名空间       + 类名     +    Trigger
            FSMTrigger fsmTr = Activator.CreateInstance(type) as FSMTrigger;
            triggers.Add(fsmTr);
        }
        /// <summary>
        /// 进入状态
        /// </summary>
        public virtual void EnterState(FSMBase fSMBase) { }
        /// <summary>
        /// 在状态中
        /// </summary>
        public virtual void ActionState(FSMBase fSMBase) { }
        /// <summary>
        /// 退出状态
        /// </summary>
        public virtual void ExitState(FSMBase fSMBase) { }

    }
}
