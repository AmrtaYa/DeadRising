using System;
using GJC.Helper;
using UnityEngine;
using UnityEngine.UI;
using Unit = MiYue.Unit;

namespace Global
{
    /// <summary>
    /// 
    /// </summary>
    public class WallUIControl : Control
    {
       [HideInInspector ] public Unit Wall;
       [HideInInspector ] public Text HpText;
       [HideInInspector ]public Image HpImage;
       [HideInInspector ]  public WallUIView view;

        protected override void Init()
        {
            HpText = transform.FindTheTfByName<Text>("Text");
            HpImage = transform.FindTheTfByName<Image>("ReallyHP");
            transform.localScale = Vector3.one;
            view = new WallUIView();
        }

        private void Update()
        {
            view.UpdateTheView(this);
        }
    }

    public class WallUIView : View
    {

        public void UpdateTheView(WallUIControl ctrl)
        {
            if (ctrl == null) return;
            WallUIControl wallUIControl = ctrl as WallUIControl;
            if (wallUIControl.Wall.Data.MaxHP <= 0) return;
            wallUIControl.HpText.text = $"城墙血量:{wallUIControl.Wall.Data.HP}/{wallUIControl.Wall.Data.MaxHP}";
            wallUIControl.HpImage.fillAmount = wallUIControl.Wall.Data.HP / wallUIControl.Wall.Data.MaxHP;
        }
    }
}