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
    /// ����״̬��
    /// </summary>
    public class FSMBase : MonoBehaviour
    {
        #region ״̬������Ķ���

        private List<FSMState> State; //�����ɫ������״̬
        private FSMState currentState; //��ǰ״̬
        [Tooltip("Ĭ��״̬���")] public FSMStateID defaultStateID;
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
        /// ���ʹ�ã������е���һ�� ����ָ����ȡ�ĸ����ñ�
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
        /// ��ʼ�������¼�
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void InitEvent()
        {
            animEvent.AttackAction = unit.Attack;
        }

        /// <summary>
        /// ��ʼ�����
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
        /// Ĭ��״̬��ʼ��  
        /// </summary>
        private void DefaultStateInit() //��config״̬���Ժ󣬽���Ѱ��Ĭ�ϵ�state���ѵ�ǰ״̬�����������ִ�����Ľ���״̬����
        {
            defaultState = State.Find(s => s.stateID == defaultStateID);
            currentState = defaultState;
            if(defaultState==null)
                Debug.LogError("�����е���һ��FsmBase�е�InitAll ����ָ����ȡ�ĸ����ñ�");
            currentState.EnterState(this);
        }

        private void Update()
        {
            text_Fsm = currentState.stateID; //����
            targetTr = FindTarget();
            towerTr=  FindTower();
            if (currentState == null) return;
            currentState.Reason(this); //ÿ֡��⣬�Ƿ��л�״̬
            currentState.ActionState(this); //״̬ÿ֡���Ķ���
        }

        /// <summary>
        /// ״̬�������� ��״̬�ӽ�ȥ
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
                        //   Debug.Log("<color=#FF0000>" + "������" + "</color>");
                        fsmStateID = defaultStateID;
                    }

                    newState.AddMap(fsmTriggerID, fsmStateID);
                }
            }
        }

        public void ChangeState(FSMStateID stateID)
        {
            //�ı�currentState
            currentState.ExitState(this);
            currentState = stateID == defaultStateID ? defaultState : State.Find(s => s.stateID == stateID);
            currentState.EnterState(this);
        }

        #endregion


        #region Ϊ״̬��������׼���Ķ���

        //�������Ƿ���ʹ�õ�
        [HideInInspector] public Animator anim;
        [HideInInspector] public AnimEvent animEvent;
        [HideInInspector] public string animMotionName = "Motion";
        [HideInInspector] public UnityEngine.AI.NavMeshAgent navAgent;
        [HideInInspector] public UnityEngine.AI.NavMeshObstacle navObstacle;
        [HideInInspector] public Transform targetTr;
        [HideInInspector] public Transform towerTr;
        [HideInInspector] public Unit unit;
        public bool IfFInishPatrolling = true; //�Ƿ����Ѳ��
        public PatrollingMode patrollingMode = PatrollingMode.LoopPatrolling; //Ѳ��״̬ Ĭ��ѭ��
        [Tooltip("Ѳ��Ŀ���")] public Transform[] patrollingTarget;
        private Transform smallWall; //��ʬר��

        private Transform FindTower()//denfenceר��
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


            navAgent.SetDestination(pos); //��Ϊ����׷��״̬��˵���ҵ�Ŀ���Ǵ����˵ģ���һ������Ϊnull����Ȼ���ܻ���bug����ôһ֡��ⲻ��
            navAgent.stoppingDistance = stopDis;
            navAgent.speed = moveSpeed;
        }

        #endregion
    }
}