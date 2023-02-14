using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using FSM;
using GJC.Helper;
using Unity.VisualScripting;
using UnityEngine;

namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
    public class Bullet : MonoBehaviour
    {
        public Unit AttackMan; //攻击者
        public float BulletSpeed = 30f;
        public float existTime = 10f; //最多存在时间
        private Coroutine OverFlyTimeCor;
        private BoxCollider BoxCollider;
        public TrailRenderer TrailRenderer;
        private void Start()
        {
            BoxCollider = GetComponent<BoxCollider>();
        }

        private void OnEnable()
        {
            OverFlyTimeCor = CoroutineManager.Instance.StartCor(OverFlyTime());
            if (BoxCollider != null)
                BoxCollider.enabled = true;
        }

        private void OnDisable()
        {
            CoroutineManager.Instance.StopCor(OverFlyTimeCor);
        }

        private void OnTriggerEnter(Collider other)
        {
            var unit = other.GetComponent<Unit>();
            if (unit == null) return;
            if (unit.Data.unitType == UnitType.Denfence || unit.Data.unitType == UnitType.CanDestoryWall) return;
            if (unit.Data.HP <= 0) return;
            BoxCollider.enabled = false;
            unit.Damage(AttackMan.Data.ATK);
            GameObjectPool.Instance.Release(gameObject);
        }

        private IEnumerator OverFlyTime()
        {
            yield return new WaitForSeconds(existTime);
            GameObjectPool.Instance.Release(gameObject);
        }

        private void Update()
        {
            transform.position += transform.forward * BulletSpeed * Time.deltaTime;
        }
    }
}