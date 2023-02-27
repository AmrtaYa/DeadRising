using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
   public class SmallWall : Unit
   {
       private void Start()
       {
           Data = new UnitData() { MaxHP = 10000,HP = 10000};
       }

       public override void Attack()
       {
           
       }

       public override void Dead()
       {
          
       }

       public override void Damage(float atkDamage)
       {
           FightMainGameEngine.Instance.DestroyWall.Damage(atkDamage);
           
       }
   }
}
