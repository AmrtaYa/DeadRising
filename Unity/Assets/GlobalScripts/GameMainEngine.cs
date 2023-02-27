using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using Liluo.BiliBiliLive;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiYue
{
    public class GameMainEngine : SingleTon<GameMainEngine>
    {
        public int roomID = 5137679;

        public override void init()
        {
            base.init();
            DontDestroyOnLoad(gameObject);
            GameMainData.Init();
            GameSceneManager.LoadSceneAnsyc(SceneType.ChooseRoomID);
        }

        public void BiliBiliRoomInit(int RoomID)
        {
            roomID = RoomID;
            BiliBiliLive.InitAction += BiliBiliInit;
            BiliBiliLive.Connect(roomID);
        }

        private void BiliBiliInit(BiliBiliLiveRequest liveRequest)
        {
            liveRequest.OnDanmuCallBack += DanmuProcess;
            liveRequest.OnGiftCallBack += GiftProcess;
            liveRequest.OnRoomViewer += RoomProcess;
        }

        private void RoomProcess(int obj)
        {
            
        }

        private void GiftProcess(BiliBiliLiveGiftData gift)
        {
            switch (GameSceneManager.GetCurrentScene())
            {
                case SceneType.DeadRise:
                    FightMainGameEngine.Instance.GiftBuffer(gift);
                    break;
            }
            
        }

        private void DanmuProcess(BiliBiliLiveDanmuData danmu)
        {
            switch (GameSceneManager.GetCurrentScene())
            {
                case SceneType.DeadRise:
                    FightMainGameEngine.Instance.CreateRole(danmu);
                    break;
            }
        }
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
                if (UnitManager.Instance.GetLenth(UnitType.Denfence) == 0) return 0;
                return iDGoToTower % UnitManager.Instance.GetLenth(UnitType.Denfence);
            }
        }

        public static float GoalToCommonMoney = 10F;

        private static int iDGoToTower = -1;
        public static float LevelEXP = 5;
    }
}