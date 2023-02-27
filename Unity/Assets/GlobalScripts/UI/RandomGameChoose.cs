using System;
using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiYue
{
    public class RandomGameChoose : MonoBehaviour
    {
        private TMP_Text GameName;
        private TMP_Text timeEnd;
        private Coroutine cor;
        private float indexTime = 0;
        public float waitTime = 6f;
        public float lastWaitTime = 5f;

        private void Start()
        {
            GameName = transform.FindTheTfByName<TMP_Text>("GameList");
            timeEnd = transform.FindTheTfByName<TMP_Text>("TimeEnd");
            cor = CoroutineManager.Instance.StartCor(RandomLottery());
        }

        private IEnumerator RandomLottery()
        {
            List<string> gameList = new List<string>();
            foreach (var value in Enum.GetNames(typeof(SceneType)))
            {
                if (value == SceneType.SetUp.ToString() || value == SceneType.RandomGameScene.ToString() ||
                    value == SceneType.ChooseRoomID.ToString())
                    continue;
                gameList.Add(value);
                print(value);
            }

            if (gameList.Count <= 0)
                Debug.LogError("没有游戏");
            int currentGameID = 0;
            while (indexTime <= waitTime)
            {
                yield return new WaitForSeconds(0.1f);
                indexTime += 0.1f;
                GameName.text = gameList[(currentGameID) % (gameList.Count)];
                currentGameID++;
            }
            GameName.text = gameList[Random.Range(0, gameList.Count)];
            string gameName = GameName.text;
            CoroutineManager.Instance.StartCor(TimeCalc());
            yield return new WaitForSeconds(lastWaitTime);
            //加载场景
            GameSceneManager.LoadSceneAnsyc(Enum.Parse<SceneType>(gameName));
        }

        private IEnumerator TimeCalc()
        {
            int index = (int)lastWaitTime;
            while (index >= 0.1f)
            {
                if (index <= 1.1f)
                {
                    timeEnd.color = Color.red;
                }

                timeEnd.text = index.ToString();
                yield return new WaitForSeconds(1f);
                index--;
            }
        }
    }
}