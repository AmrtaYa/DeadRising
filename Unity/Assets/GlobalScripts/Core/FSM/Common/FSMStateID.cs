using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    /// <summary>
    /// ����״̬��״̬���
    /// </summary>
    public enum FSMStateID
    {
        None,
        Default,
        Dead,
        Idle,
        Pursuit,//׷��
        Attacking,
        Patrolling  ,//Ѳ��
        PrePareAttack,
        OnTheTower,
        FindTheTower,
        NoOneTower
    }
}
