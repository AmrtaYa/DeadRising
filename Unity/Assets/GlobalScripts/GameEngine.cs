using System;
using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using UnityEngine;

namespace MiYue
{
    /// <summary>
    /// 写到了，进入炮台 系统 2023.2.10停止更新      
    /// </summary>
    public class GameEngine : SingleTon<GameEngine>
    {
        public DestroyWall DestroyWall;

        public override void init()
        {
            base.init();
            DestroyWall = GameObject.FindObjectOfType<DestroyWall>();
            UnitManager.Instance.Init();
            //测试
            for (int i = 0; i < 1; i++)
            {
                UnitManager.Instance.AddPlayer(DataConfigCSV.Instance[UnitType.Zombie, CareerType.Zombie],
                    SpawnManager.Instance.RandomGetSpawnPoint(SpawnType.ZombieSpawn).position, Quaternion.identity);
            }

            for (int i = 0; i < 30; i++)
            {
                UnitManager.Instance.AddPlayer(DataConfigCSV.Instance[UnitType.Denfence, CareerType.Alien],
                    SpawnManager.Instance.RandomGetSpawnPoint(SpawnType.DenfenceSpawn).position, Quaternion.identity);
            }
        }

        // private void FixedUpdate()
        // {
        //     foreach (var Sintower in UnitManager.Instance.GetAllTypeUnit(UnitType.Tower))
        //     {
        //         CommonAtkTower tower = Sintower.GetComponent<CommonAtkTower>();
        //      
        //     }
        // }
    }

    public static class GameGlobalSetting
    {
        public static float NavStopDis = 1f;
        public static float ZombieSpawnTime = 10f;
        public static float UserTowerTime = 10f;

        public static int AllPlayerNumWhereNoOnTheTower
        {
            get
            {
                return UnitManager.Instance.GetAllTypeUnit(UnitType.Denfence).FindObjects(t =>
                {
                    return !t.GetComponent<CommonDenfence>().IfOnTheTower;
                }).Length;
            }
        }

        public static int IDGoToTower
        {
            get
            {
                iDGoToTower++;
                return iDGoToTower % UnitManager.Instance.GetLenth(UnitType.Denfence);
            }
        }

        private static int iDGoToTower = -1;
        public static float LevelEXP = 5;
    }

    public enum Tag
    {
        AttackSpawn
    }
}