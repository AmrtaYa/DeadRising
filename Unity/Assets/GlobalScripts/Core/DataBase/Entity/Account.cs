using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;

namespace MiYue
{
    public class AccountManager
    {
  

        /// <summary>
        /// money也可以是负数，扣钱哦
        /// </summary>
        /// <param name="UID"></param>
        /// <param name="num"></param>
        /// <param name="mt"></param>
        public static void UpdateMoney(int UID, int num, MoneyType mt = MoneyType.CommonMoney)
        {
            Account account = GameMainData.connection.Find<Account>(t => t.UID == UID);
            if (account == null) Debug.LogError("没有此人");
            switch (mt)
            {
                case MoneyType.CommonMoney:
                    account.CommonMoney += num;
                    break;
                case MoneyType.SpecifitMoney:
                    account.SpecifitMoney += num;
                    break;
            }

            GameMainData.connection.Update(account);
        }
    }

    public enum MoneyType
    {
        CommonMoney,
        SpecifitMoney
    }

    public class Account
    {
        [PrimaryKey] public int UID { get; set; }
        public int CommonMoney { get; set; }
        public int SpecifitMoney { get; set; }
    }
}