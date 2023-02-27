using System;
using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using Global;
using TMPro;
using UnityEngine;

namespace MiYue
{
    public class HitWindowsControl : Control
    {
        public TMP_Text strText;
        public HitWindowsView view;
        public HitWindowsModel model;
        public float keepTime = 3.5f;
        private float indexTime = 0;
        protected override void OnEnable()
        {
            base.OnEnable();
            indexTime = 0f;
            UiAnimFactory.Instance[UiAnimType.AcrossIn].PlayAnim<TMP_Text>(this);
        }

        private void Update()
        {
            indexTime += Time.deltaTime;
            if (indexTime >= keepTime)
            {
                indexTime = -999999;
                UiAnimFactory.Instance[UiAnimType.AcrossOut].PlayAnim<TMP_Text>(this);
            }
        }

        protected override void Init()
        {
           
        }

        /// <summary>
        /// 外部手动初始化
        /// </summary>
        public void init()
        {
            strText = transform.FindTheTfByName<TMP_Text>("StrText");
            model = new HitWindowsModel(this);
            view = new HitWindowsView(this);
        }
    }

    public class HitWindowsView : View
    {
        private HitWindowsControl hitWindowsControl;

        public void ShowText(string str)
        {
            hitWindowsControl.strText.text = str;
        }

        public HitWindowsView(HitWindowsControl CTRL)
        {
            hitWindowsControl = CTRL;
        }
    }

    public class HitWindowsModel : Model
    {
        private HitWindowsControl hitWindowsControl;

        public HitWindowsModel(HitWindowsControl CTRL)
        {
            hitWindowsControl = CTRL;
        }
    }
}