using System;
using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MiYue
{
    public static class GameSceneManager
    {
        private static AsyncOperation ao;

        public static SceneType GetCurrentScene()
        {
            Scene currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            return (SceneType)Enum.Parse(typeof(SceneType), currentScene.name);
        }

        public static void LoadScene(SceneType sceneType)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneType.ToString());
            Time.timeScale = 1;
            UIManager.Instance.ClearLayerUI();
        }

        public static AsyncOperation LoadSceneAnsyc(SceneType sceneType)
        {
            Time.timeScale = 1;
            return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneType.ToString());
        }

        public static AsyncOperation LoadSceneAnsyc(SceneType sceneType, float time)
        {
            ao = null;
            CoroutineManager.Instance.StartCor(LoadSceneCor(sceneType, time));
            return ao;
        }

        private static IEnumerator LoadSceneCor(SceneType sceneType, float time)
        {
            yield return new WaitForSeconds(time);
            UIManager.Instance.ClearLayerUI();
            ao = LoadSceneAnsyc(sceneType);
        }
    }

    public enum SceneType
    {
        SetUp,
        ChooseRoomID,
        RandomGameScene,
        DeadRise,
        TerritorialWars,
    }
}