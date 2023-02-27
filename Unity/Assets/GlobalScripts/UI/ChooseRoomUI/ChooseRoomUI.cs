using System;
using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using TMPro;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.UI;

namespace MiYue
{
    public class ChooseRoomUI : MonoBehaviour
    {
        [SerializeField] private TMP_InputField roomIDInput;
        [SerializeField] private Button playBtn;

        private void Awake()
        {
            roomIDInput = transform.FindTheTfByName<TMP_InputField>("RoomIDInput");
            playBtn = transform.FindTheTfByName<Button>("Play");
            playBtn.onClick.AddListener(PlayEvent);
        }

        private void PlayEvent()
        {
            string id = roomIDInput.text;
            if (string.IsNullOrEmpty(id))
            {
               var windows =  UIManager.Instance.AddNewUIWithPool("HitWindowsTwo",UILayer.Mid,new Vector3(Screen.width/2,120,0)).GetComponent<HitWindowsControl>();
               windows.init();
               windows.view.ShowText("RoomID Can't be empty");
                return;
            }
            GameMainEngine.Instance.BiliBiliRoomInit(int.Parse(id));
            GameSceneManager.LoadSceneAnsyc(SceneType.RandomGameScene);
        }
    }
}