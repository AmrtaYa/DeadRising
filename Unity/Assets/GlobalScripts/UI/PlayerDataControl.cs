using System;
using System.Collections;
using GJC.Helper;
using MiYue;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Unit = MiYue.Unit;

namespace Global
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerDataControl : Control
    {
        public Unit Owner;
        public Text PlayerDataText;
        public PlayerDataView View;
        private Coroutine InitBaseInfo;
        protected override void Init()
        {
            if (View != null) return;
            PlayerDataText = transform.FindTheTfByName<Text>("PlayerData");
            View = new PlayerDataView(this);
        }

        private void Awake()
        {
            Init();
        }

        private void Update()
        {
            View.UpdateTheView();
            if (Owner.Data.HP <=0)
                GameObjectPool.Instance.Release(gameObject);
        }

        private void OnEnable()
        {
            InitBaseInfo=  CoroutineManager.Instance.StartCor(InitUIBaseInfo());
        }

        private IEnumerator InitUIBaseInfo()
        {
            yield return Owner != null;
            switch (Owner.Data.unitType)
            {
                case  UnitType.Denfence:
                    PlayerDataText.color = Color.green;
                    break;
                case  UnitType.Zombie:
                    PlayerDataText.color = Color.red;
                    break;
            }
        }

        private void OnDisable()
        {
            Owner = null;
            CoroutineManager.Instance.StopCor(InitBaseInfo);
            InitBaseInfo = null;
        }
    }

    public class PlayerDataView : View
    {
        private PlayerDataControl contrl;

        public PlayerDataView(PlayerDataControl ctrl)
        {
            contrl = ctrl;
        }

        public void UpdateTheView()
        {
            contrl.PlayerDataText.text =
                $"{contrl.Owner.Data.PlayerName}\n{contrl.Owner.Data.Level}\n{contrl.Owner.Data.HP}";
            contrl.transform.position = Camera.main.WorldToScreenPoint(contrl.Owner.transform.position);
        }

 
    }

    public class PlayerDataModel : Model
    {
    }
}