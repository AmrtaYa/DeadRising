using System;
using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using MiYue;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Unit = MiYue.Unit;

namespace FSM
{
    /// <summary>
    /// 有限状态机
    /// </summary>
    public class FSMBase : MonoBehaviour
    {
        #region 状态机本身的东西

        private List<FSMState> State; //这个角色的所有状态
        private FSMState currentState; //当前状态
        [Tooltip("默认状态编号")] public FSMStateID defaultStateID;
        private FSMState defaultState;
        [SerializeField] private FSMStateID text_Fsm;

        private void OnEnable()
        {
            smallWall = null;
            if (State != null)
            {
                DefaultStateInit();
            }
        }

        /// <summary>
        /// 如果使用，请自行调用一次 并且指定读取哪个配置表
        /// </summary>
        /// <param name="config"></param>
        public void InitAll(Dictionary<string, Dictionary<string, string>> config)
        {
            InitComponet();
            FSMConfig(config);
            DefaultStateInit();
            InitEvent();
        }

        /// <summary>
        /// 初始化动画事件
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void InitEvent()
        {
            animEvent.AttackAction = unit.Attack;
        }

        /// <summary>
        /// 初始化组件
        /// </summary>
        private void InitComponet()
        {
            State = new List<FSMState>();
            anim = this.GetComponentInChildren<Animator>();
            if (anim != null) animEvent = anim.gameObject.AddComponent<AnimEvent>();
            navAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            navObstacle = GetComponent<UnityEngine.AI.NavMeshObstacle>();
            ChangeNav(true);
            unit = GetComponent<Unit>();
        }

        /// <summary>
        /// 默认状态初始化  
        /// </summary>
        private void DefaultStateInit() //在config状态完以后，进行寻找默认的state，把当前状态变成他，并且执行他的进入状态方法
        {
            defaultState = State.Find(s => s.stateID == defaultStateID);
            currentState = defaultState;
            if(defaultState==null)
                Debug.LogError("请自行调用一次FsmBase中的InitAll 并且指定读取哪个配置表");
            currentState.EnterState(this);
        }

        private void Update()
        {
            text_Fsm = currentState.stateID; //测试
            targetTr = FindTarget();
            towerTr=  FindTower();
            if (currentState == null) return;
            currentState.Reason(this); //每帧检测，是否切换状态
            currentState.ActionState(this); //状态每帧做的东西
        }

        /// <summary>
        /// 状态机的配置 把状态加进去
        /// </summary>
        public void FSMConfig(Dictionary<string, Dictionary<string, string>> config)
        {
            var fsmDir = config;
            foreach (var state in fsmDir)
            {
                Type type = Type.GetType("FSM." + state.Key + "State");
                var newState = Activator.CreateInstance(type) as FSMState;
                State.Add(newState);
                foreach (var item in state.Value) // item.key == trigger  item.value == state
                {
                    FSMTriggerID fsmTriggerID = (FSMTriggerID)Enum.Parse(typeof(FSMTriggerID), item.Key);
                    FSMStateID fsmStateID = (FSMStateID)Enum.Parse(typeof(FSMStateID), item.Value);
                    if (fsmStateID == FSMStateID.Default)
                    {
                        //   Debug.Log("<color=#FF0000>" + "调用了" + "</color>");
                        fsmStateID = defaultStateID;
                    }

                    newState.AddMap(fsmTriggerID, fsmStateID);
                }
            }
        }

        public void ChangeState(FSMStateID stateID)
        {
            //改变currentState
            currentState.ExitState(this);
            currentState = stateID == defaultStateID ? defaultState : State.Find(s => s.stateID == stateID);
            currentState.EnterState(this);
        }

        #endregion


        #region 为状态，条件，准备的东西

        //这两个是方便使用的
        [HideInInspector] public Animator anim;
        [HideInInspector] public AnimEvent animEvent;
        [HideInInspector] public string animMotionName = "Motion";
        [HideInInspector] public UnityEngine.AI.NavMeshAgent navAgent;
        [HideInInspector] public UnityEngine.AI.NavMeshObstacle navObstacle;
        [HideInInspector] public Transform targetTr;
        [HideInInspector] public Transform towerTr;
        [HideInInspector] public Unit unit;
        public bool IfFInishPatrolling = true; //是否完成巡逻
        public PatrollingMode patrollingMode = PatrollingMode.LoopPatrolling; //巡逻状态 默认循环
        [Tooltip("巡逻目标点")] public Transform[] patrollingTarget;
        private Transform smallWall; //僵尸专用

        private Transform FindTower()//denfence专用
        {
            if (unit == null) return null;
            if (unit.Data.unitType != UnitType.Denfence) return null;
            CommonDenfence denfence = unit as CommonDenfence;
            if (denfence == null) return null;
            if (denfence.targetTower == null) return null;
            return denfence.targetTower.transform;
        }

        public void SetAnim(MotionType motionType)
        {
            anim.SetInteger(animMotionName, (int)motionType);
        }

        public void ChangeNav(bool agentBool)
        {
            if (navAgent == null || navObstacle == null)
            {
                return;
            }

            navAgent.enabled = agentBool;
            navObstacle.enabled = !agentBool;
        }

        public Transform FindTarget()
        {
            GameObject[] allEnemy;
            GameObject wall;
            switch (unit.Data.unitType)
            {
                case UnitType.Denfence:
                case UnitType.Tower:
                    allEnemy = UnitManager.Instance.GetAllTypeUnit(UnitType.Zombie);
                    break;
                case UnitType.Zombie:
                    allEnemy = UnitManager.Instance.GetAllTypeUnit(UnitType.Denfence);
                    if (GameEngine.Instance.DestroyWall != null)
                        if (GameEngine.Instance.DestroyWall.Data.HP > 0)
                        {
                            if (smallWall == null)
                                smallWall = SpawnManager.Instance.RandomGetMinPoint(transform.position,
                                    SpawnType.ZombieAttackSpawn);
                            return smallWall;
                        }

                    break;
                default:
                    allEnemy = null;
                    break;
            }

            if (allEnemy != null)
            {
                allEnemy = allEnemy.FindObjects(t =>
                        Vector3.Distance(t.transform.position.XyzToX0z(), transform.position.XyzToX0z()) <=
                        unit.Data.FindRange)
                    .FindObjects(t => t.GetComponent<Unit>().Data.HP > 0);
                GameObject singleEnemy = null;
                if (allEnemy.Length > 0)
                    singleEnemy = allEnemy.GetMin(t =>
                        Vector3.Distance(t.transform.position.XyzToX0z(), transform.position.XyzToX0z()));
                return singleEnemy == null ? null : singleEnemy.transform;
            }

            return null;
        }

        public void Move(Vector3 pos, float stopDis, float moveSpeed)
        {
            if (navAgent == null)
            {
                return;
            }


            navAgent.SetDestination(pos); //因为进入追逐状态，说明找到目标是触发了的，那一定不会为null，虽然可能会有bug有那么一帧检测不到
            navAgent.stoppingDistance = stopDis;
            navAgent.speed = moveSpeed;
        }

        #endregion
    }
}