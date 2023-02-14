using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace FSM
{
    /// <summary>
    /// 
    /// </summary>
   public class AnimEvent : MonoBehaviour
    {
        public Action AttackAction;

        public void Attack()
        {
            if (AttackAction != null)
                AttackAction();
        }
    }
}
