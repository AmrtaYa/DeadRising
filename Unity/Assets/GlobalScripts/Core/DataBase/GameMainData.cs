using System.Collections;
using System.Collections.Generic;
using SQLite4Unity3d;
using UnityEngine;

namespace MiYue
{
	public static class GameMainData
	{
		private  const string ConnentStr =@"C:/New folder/Test/DeadRising-master/DataBase/GameMain.db";
		public static SQLiteConnection connection;
		static GameMainData()
		{
			SQLiteConnectionString connentStr = new SQLiteConnectionString(ConnentStr,false);
			 connection = new SQLiteConnection(connentStr.DatabasePath,
				SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite);
		}

		public static void Init()
		{
		
		}

		public static bool CheckIfHas(int uid)
		{
			UnitData data;
			data = connection.Find<UnitData>(t => t.UID == uid);
			return data != null;
		}
		public static UnitData RegisterAccount(UnitData unitData)
		{
			UnitData ud = new UnitData() { UID = unitData.UID, PlayerName = unitData.PlayerName };
			connection.Insert(ud);
			connection.Insert(new Account() { UID = unitData.UID, CommonMoney = 0, SpecifitMoney = 0 });
			return ud;
		}
	}
}

