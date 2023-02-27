using System;
using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;
namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
   public class CommonDenfence : Unit
   {
       public FSMBase FsmBase;
       public CommonAtkTower targetTower;
       public bool IfOnTheTower = false;
       protected override void Awake()
       {
           base.Awake();
       }
       protected virtual void Start()
       {
           FsmBase = GetComponent<FSMBase>();
           FsmBase.defaultStateID = FSMStateID.Idle;
           FsmBase.InitAll(FSMConfigCSV.Instance.DenfenceFsmConfig);
       }
       

       public void StartCalcTowerTime(CommonAtkTower tower)
       {
           if (targetTower != null) return;
           CoroutineManager.Instance.StartCor(CalcTowerTime(tower));
       }

       private IEnumerator CalcTowerTime(CommonAtkTower tower)
       {
           targetTower = tower;
           float indexTIme = 0;
           targetTower.unit = this;
           while (true)
           {
               yield return new  WaitForFixedUpdate();
               if (IfOnTheTower) break;
           }
         
           targetTower.onTheTower = true;
           while (indexTIme<=GameGlobalSetting.UserTowerTime)
           {
               yield return new WaitForSeconds(1);
               indexTIme += 1;
           }
           transform.SetParent(null);
           targetTower.onTheTower = false;
           targetTower.unit = null;
           IfOnTheTower = false;
           targetTower = null;
       }

       public virtual void OnEnable()
       {
           Data.HP = Data.MaxHP;
       }

       public override void Attack()
       {
           MoneyManager.GetGoal(1);
       }

       public override void Dead()
       {
          
       }

       public override void Damage(float atkDamage)
       {
          
       }

    
   }
}
