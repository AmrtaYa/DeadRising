using System;
using System.Collections;
using System.Collections.Generic;
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
        private Dictionary<UnitType, List<GameObject>> playerList = new Dictionary<UnitType, List<GameObject>>();

        public void Init()
        {
            foreach (var value in Enum.GetValues(typeof(UnitType)))
            {
                playerList.Add((UnitType)value, new List<GameObject>());
            }

            InitTower();
            InitDestoryWall();
        }

        public GameObject GetPlayer(int index, UnitType ut = UnitType.Denfence)
        {
            return playerList[ut][index];
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
            return playerList[unitType].ToArray();
        }

        public GameObject AddPlayer(UnitData data, Vector3 pos, Quaternion quaternion)
        {
            CareerType careerType = data.careerType;
            GameObject init = ResConfigCSV.Instance.GetInstanceWithPool(careerType.ToString(), pos, quaternion);
            data.HP = data.MaxHP;
            Unit initUnit = init.GetComponent<Unit>();
            initUnit.Data = new UnitData(data);
            if (!playerList[data.unitType].Contains(init))
            {
                playerList[data.unitType].Add(init);
            }
            if (data.unitType !=UnitType.CanDestoryWall )
            {
                if (data.unitType != UnitType.Tower)
                {
                    var UI=  UIManager.Instance.AddNewUIWithPool("PlayerDataShow");
                    UI.GetComponent<PlayerDataControl>().Owner = initUnit;
                }
            }

            return init;
        }

        public void RemovePlayer(UnitData data)
        {
        }
    }
}