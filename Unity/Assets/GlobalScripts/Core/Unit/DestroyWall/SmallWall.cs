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
           Data = new UnitData() { MaxHP = -100,HP = -100};
       }

       public override void Attack()
       {
           
       }

       public override void Dead()
       {
          
       }

       public override void Damage(float atkDamage)
       {
           GameEngine.Instance.DestroyWall.Damage(atkDamage);
           
       }
   }
}
