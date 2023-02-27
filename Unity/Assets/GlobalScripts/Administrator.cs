using System;
using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using MiYue;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AdministratorNs
{
    /// <summary>
    /// 管理员
    /// </summary>
    public class Administrator : MonoBehaviour
    {
        public Transform administratorUI;
        public Action InputCheck;


        private Button exitButton;
        private Button changeGame;
        private TMP_Dropdown dropDown; //下拉框

        private void Awake()
        {
            administratorUI = transform.FindTheTfByName("AdminstratorUI");
            exitButton = transform.FindTheTfByName<Button>("Exit");
            dropDown = transform.FindTheTfByName<TMP_Dropdown>("Dropdown");
            changeGame = transform.FindTheTfByName<Button>("ChangeGame");
            administratorUI.gameObject.SetActive(false);
            InputCheck += BaseInput;

            exitButton.onClick.AddListener(() => { administratorUI.gameObject.SetActive(false); });
            changeGame.onClick.AddListener(ChangeGame);
            var sceneList = Enum.GetNames(typeof(SceneType));
            dropDown.options.Clear(); 
            print(sceneList.Length);
            for (int i = 0; i < sceneList.Length; i++)
            {
                dropDown.options.Add(new TMP_Dropdown.OptionData(){ text = sceneList[i]});
            }
        }

        private void ChangeGame()
        {
            string sceneName =dropDown.options[ dropDown.value].text;
            GameSceneManager.LoadSceneAnsyc(Enum.Parse<SceneType>(sceneName));
            administratorUI.gameObject.SetActive(false);
        }

        private void BaseInput()
        {
            if (Input.GetKeyDown(KeyCode.F11))
            {
                administratorUI.gameObject.SetActive(!administratorUI.gameObject.activeInHierarchy);
            }
        }

        private void Update()
        {
            InputCheck();
        }
    }
}