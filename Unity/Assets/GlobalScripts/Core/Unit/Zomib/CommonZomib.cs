using System;
using System.Collections;
using System.Collections.Generic;
using FSM;
using GJC.Helper;
using UnityEngine;

namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
    public class CommonZomib : Unit
    {
        public FSMBase FsmBase;
        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            FsmBase = GetComponent<FSMBase>();
            FsmBase.defaultStateID = FSMStateID.Idle;
            FsmBase.InitAll(FSMConfigCSV.Instance.FsmConfig);
        }

        protected virtual void OnEnable()
        {
            Data.HP = Data.MaxHP;
        }

        public override void Attack()
        {
            if (FsmBase.targetTr == null) return;
            Unit enemyUnit = FsmBase.targetTr.GetComponent<Unit>();
            if (enemyUnit == null)
            {
                return;
            }

            enemyUnit.Damage(Data.ATK);
        }

        public override void Dead()
        {
            FsmBase.SetAnim(MotionType.Dead);
            CoroutineManager.Instance.StartCor(Spawn());
        }

        /// <summary>
        /// 复活！
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private IEnumerator Spawn()
        {
            yield return new WaitForSeconds(3F);
            GameObjectPool.Instance.Release(gameObject);
            yield return new WaitForSeconds(GameGlobalSetting.ZombieSpawnTime);
            UnitManager.Instance.AddPlayer(Data,
                SpawnManager.Instance.RandomGetSpawnPoint(SpawnType.ZombieSpawn).position, Quaternion.identity);
        }

        public override void Damage(float atkDamage)
        {
            Data.HP -= atkDamage;
            if (Data.HP <= 0) Dead();
        }
    }
}