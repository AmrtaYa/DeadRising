using System;
using System.Collections;
using System.Collections.Generic;
using Global;
using UnityEngine;
using UnityEngine.AI;

namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
    public class DestroyWall : Unit
    {
        private WallUIControl UIControl;
        private Animation Animation;
        private NavMeshObstacle Obstacle;
        private BoxCollider collider;
        protected override void Awake()
        {
            base.Awake();
        }
        private void Start()
        {
            Animation = GetComponent<Animation>();
            Obstacle = GetComponent<NavMeshObstacle>();
            collider = GetComponent<BoxCollider>();
            Obstacle.enabled = true;
            collider.enabled = true;
            if (FightMainGameEngine.Instance.DestroyWall == null)
                FightMainGameEngine.Instance.DestroyWall = this;
            if (UIControl == null)
            {
                GameObject ui = UIManager.Instance.AddNewUI("WallHP",UILayer.Mid,new Vector3(Screen.width/2,0));
                ui.transform.localScale = 0.3F*Vector3.one;
                UIControl = ui.GetComponent<WallUIControl>();
                UIControl.Wall = this;
            }
        }

        public override void Attack()
        {
        }

        public override void Dead()
        {
            Animation.Play("DestoryWallDead");
            Obstacle.enabled = false;
            collider.enabled = false;
        }

        public override void Damage(float atkDamage)
        {
            Data.HP -= atkDamage;
            if(Data.HP<=0)
                Dead();
        }
    }
}