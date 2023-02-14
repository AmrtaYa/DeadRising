using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using Unity.VisualScripting;
using UnityEngine;

namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
    public class Alien : CommonDenfence
    {
        public Transform LeftGun;
        public Transform RightGun;
        protected override void Start()
        {
            base.Start();
            LeftGun = transform.FindTheTfByName("ElectricGunDefault_Left").FindTheTfByName("ElectricGun");
            RightGun = transform.FindTheTfByName("ElectricGunDefault").FindTheTfByName("ElectricGun");
        }

        public override void OnEnable()
        {
            base.OnEnable();
        }

        public override void Attack()
        {
            base.Attack();
            CreateBullet(LeftGun.position - new Vector3(0, 0.5F, 0));
            CreateBullet(RightGun.position - new Vector3(0, 0.5F, 0));
        }

        public override void Damage(float atkDamage)
        {
            base.Damage(atkDamage);
            Data.HP -= atkDamage;
            if (Data.HP <= 0)
                Dead();
        }

        public override void Dead()
        {
            base.Dead();
            FsmBase.SetAnim(MotionType.Dead);
            GameObjectPool.Instance.Release(gameObject,3F);
        }

        private void CreateBullet(Vector3 pos)
        {
            var bullet = GameObjectPool.Instance.Get("PlayerBullet", ResConfigCSV.Instance.GetPrefab("Bullet"),
                pos,
                Quaternion.identity);
            if (FsmBase.targetTr != null)
                bullet.transform.forward = FsmBase.targetTr.position - transform.position;
            Bullet Singlebullet = bullet.GetComponent<Bullet>();
            Singlebullet.AttackMan = this;
            if (Singlebullet.TrailRenderer == null)
                Singlebullet.TrailRenderer = Singlebullet.GetComponent<TrailRenderer>();
            Singlebullet.TrailRenderer.Clear();
        }
    }
}