using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MiYue
{
    public class SinglePlayerMoneyManager
    {
        public int goal;
        public Unit unit;

        public SinglePlayerMoneyManager(Unit unit)
        {
            this.unit = unit;
        }

        public void GetSpeMoney(int num)
        {
            
        }

        public void GetGoal(int num)
        {
            goal++;
            if (goal >= GameGlobalSetting.GoalToCommonMoney)
            {
                goal = 0;
                var data = GameMainData.connection.Find<UnitData>(t => t.UID == unit.Data.UID);
                if (data == null)
                {
                    if (data.UID == 0) return;
                    data = GameMainData.RegisterAccount(new UnitData()
                        { UID = unit.Data.UID, PlayerName = unit.Data.PlayerName });
                }

                Account userAccountMoney = GameMainData.connection.Find<Account>(t => t.UID == data.UID);
                userAccountMoney.CommonMoney++;
                GameMainData.connection.Update(userAccountMoney);
                Debug.Log("CommoneyMoney+1"+"\t"+unit.name);
            }
        }
    }
}