using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GJC.Helper;
using Global;
using Unity.VisualScripting;
using UnityEngine;

namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
    public class UnitManager : SharpSington<UnitManager>
    {
        private Dictionary<UnitType, List<Unit>> playerList = new Dictionary<UnitType, List<Unit>>();

        public void Init()
        {
            playerList.Clear();
            foreach (var value in Enum.GetValues(typeof(UnitType)))
            {
                playerList.Add((UnitType)value, new List<Unit>());
            }

            InitTower();
            InitDestoryWall();
        }


        public GameObject GetPlayer(int index, UnitType ut = UnitType.Denfence)
        {
            if (playerList[ut].Count == 0) return null;
            return playerList[ut][index].gameObject;
        }

        public int GetLenth(UnitType unitType)
        {
            return playerList[unitType].Count;
        }

        private void InitDestoryWall()
        {
            UnitManager.Instance.AddPlayer(DataConfigCSV.Instance[UnitType.CanDestoryWall, CareerType.CanDestoryWall],
                SpawnManager.Instance[SpawnType.WallSpawn][0].position, Quaternion.identity);
        }

        private void InitTower()
        {
            float X = 4;
            for (int i = 0; i < 3; i++)
            {
                AddPlayer(DataConfigCSV.Instance[UnitType.Tower, CareerType.Tower],
                    SpawnManager.Instance[SpawnType.TowerSpawn][i].position, Quaternion.identity);
            }
        }

        public GameObject[] GetAllTypeUnit(UnitType unitType)
        {
            return playerList[unitType].ToArray().Select(t=> t.gameObject);
        }

        public Unit AddPlayer(UnitData data, Vector3 pos, Quaternion quaternion)
        {
            CareerType careerType = data.careerType;
            GameObject init = ResConfigCSV.Instance.GetInstanceWithPool(careerType.ToString(), pos, quaternion);
            data.HP = data.MaxHP;
            Unit initUnit = init.GetComponent<Unit>();
            initUnit.Data = new UnitData(data);
            if (!playerList[data.unitType].Contains(initUnit))
            {
                playerList[data.unitType].Add(initUnit);
            }

            if (data.unitType != UnitType.CanDestoryWall)
            {
                if (data.unitType != UnitType.Tower)
                {
                    var UI = UIManager.Instance.AddNewUIWithPool("PlayerDataShow");
                    UI.GetComponent<PlayerDataControl>().Owner = initUnit;
                }
            }

            return initUnit;
        }

        public void RemovePlayer(UnitData data)
        {
        }

        /// <summary>
        /// 检查是否现在场景中有这个玩家，避免多次创建
        /// </summary>
        /// <param name="danmuUserId"></param>
        /// <returns></returns>
        public bool HasUnit(int danmuUserId)
        {
            foreach (var value in playerList.Values)
            {
                foreach (var unitValue in value)
                {
                    if (unitValue.Data.UID == danmuUserId) return true;
                }
            }
            return false;
        }
    }
}