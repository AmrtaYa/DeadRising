using System;
using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using Liluo.BiliBiliLive;
using Unity.VisualScripting;
using UnityEngine;

namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
    public class FightMainGameEngine : SingleTon<FightMainGameEngine>
    {
        public DestroyWall DestroyWall;
        public float MaxTime = 600f;
        private bool GameOverBool = false;

        public override void init()
        {
            base.init();
            DestroyWall = GameObject.FindObjectOfType<DestroyWall>();
            UnitManager.Instance.Init();
        }

        private void FixedUpdate()
        {
            if (GameOverBool) return;
            if (DestroyWall.Data.HP <= 0)
            {
                GameOverBool = true;
                GameOver(UnitType.Zombie);
            }

            if (MaxTime <= 0 && DestroyWall.Data.HP >= 0)
            {
                GameOverBool = true;
                GameOver(UnitType.Denfence);
            }
        }

        private void GameOver(UnitType denfence)
        {
            string wins = "";
            GameObject[] winsMembers = null;
            switch (denfence)
            {
                case UnitType.Zombie:
                    wins = "Zombie Win!";
                    winsMembers = UnitManager.Instance.GetAllTypeUnit(UnitType.Zombie);
                    break;
                case UnitType.Denfence:
                    wins = "Defenders Win!";
                    winsMembers = UnitManager.Instance.GetAllTypeUnit(UnitType.Denfence);
                    break;
            }

            if (winsMembers == null) return;
            foreach (var winMember in winsMembers)
            {
                UnitData data = winMember.GetComponent<Unit>().Data;
                Account userAcc = GameMainData.connection.Find<Account>(T => T.UID == data.UID);
                userAcc.CommonMoney += 5;
                GameMainData.connection.Update(userAcc);
            }

            var go = UIManager.Instance.AddNewUIWithPool("HitWindows", UILayer.Top);
            var ctrl = go.GetComponent<HitWindowsControl>();
            ctrl.init();
            ctrl.keepTime = 999;
            ctrl.strText.text = wins + "\n" + "And go to next random game soon";
            GameSceneManager.LoadSceneAnsyc(SceneType.RandomGameScene, 5f);
        }

        public void CreateRole(BiliBiliLiveDanmuData danmu)
        {
            Unit unit = null;
            if (UnitManager.Instance.HasUnit(danmu.userId)) return;
            if (danmu.content.Contains(FightMainGameSetting.zombieCreate))
            {
                unit = UnitManager.Instance.AddPlayer(DataConfigCSV.Instance[UnitType.Zombie, CareerType.Zombie],
                    SpawnManager.Instance.RandomGetSpawnPoint(SpawnType.ZombieSpawn).position, Quaternion.identity);
            }

            if (danmu.content.Contains(FightMainGameSetting.denfenceCreate_Alien))
            {
                unit = UnitManager.Instance.AddPlayer(DataConfigCSV.Instance[UnitType.Denfence, CareerType.Alien],
                    SpawnManager.Instance.RandomGetSpawnPoint(SpawnType.DenfenceSpawn).position, Quaternion.identity);
            }

            if (unit == null) return;
            unit.Data.UID = danmu.userId;
            unit.Data.PlayerName = danmu.username;
            if (GameMainData.CheckIfHas(unit.Data.UID)) return;
            GameMainData.RegisterAccount(unit.Data);
        }

        /// <summary>
        /// 礼物buff  限制于DeadRise场景
        /// </summary>
        /// <param name="gift"></param>
        public void GiftBuffer(BiliBiliLiveGiftData gift)
        {
        }
    }

    public static class FightMainGameSetting
    {
        public static string zombieCreate = "僵尸";
        public static string denfenceCreate;
        public static string denfenceCreate_Alien = "爱丽";
    }

    public enum Tag
    {
        AttackSpawn
    }
}