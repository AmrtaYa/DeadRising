using System;
using System.Collections;
using System.Collections.Generic;
using FSM;
using GJC.Helper;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(FSMBase))]
    public class CommonAtkTower : Unit
    {
        public FSMBase FsmBase;
        private Transform GunPot;
        public Transform PlayerUserTR;
        public CommonDenfence unit;
        public bool onTheTower;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            PlayerUserTR = transform.FindTheTfByName("PlayerSetPos");
            GunPot = transform.FindTheTfByName("TurretA_GunA_C");
            if (Data.HP <= 0) Data.HP = Data.MaxHP;
            FsmBase = GetComponent<FSMBase>();
            FsmBase.defaultStateID = FSMStateID.Idle;
            FsmBase.InitAll(FSMConfigCSV.Instance.TowerFsmConfig);
        }

        private void FixedUpdate()
        {
            if (unit == null)
            {
                GameObject player = UnitManager.Instance.GetPlayer(GameGlobalSetting.IDGoToTower);
                if (player != null)
                    player.GetComponent<CommonDenfence>().StartCalcTowerTime(this);
            }
        }


        private void OnEnable()
        {
            if (Data != null)
                Data.HP = Data.MaxHP;
        }

        public override void Attack()
        {
            if (unit == null) return;
            CreateBullet(GunPot.position);
        }

        private void CreateBullet(Vector3 pos)
        {
            var bullet = GameObjectPool.Instance.Get("PlayerBullet", ResConfigCSV.Instance.GetPrefab("Bullet"),
                pos,
                Quaternion.identity);
            if (FsmBase.targetTr != null)
                bullet.transform.forward = (FsmBase.targetTr.position - transform.position) +
                                           new Vector3(Random.Range(-2, 3), Random.Range(-2, 3), Random.Range(-2, 3));
            Bullet Singlebullet = bullet.GetComponent<Bullet>();
            Singlebullet.AttackMan = this;
            if (Singlebullet.TrailRenderer == null)
                Singlebullet.TrailRenderer = Singlebullet.GetComponent<TrailRenderer>();
            Singlebullet.TrailRenderer.Clear();
        }

        public override void Dead()
        {
        }

        public override void Damage(float atkDamage)
        {
        }
    }
}