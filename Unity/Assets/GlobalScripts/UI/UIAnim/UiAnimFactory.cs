using System;
using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using Unity.VisualScripting;
using UnityEngine;

namespace MiYue
{
    public class UiAnimFactory : SharpSington<UiAnimFactory>
    {
        private Dictionary<UiAnimType, IUiAnim> uiAnimDir;

        public IUiAnim this[UiAnimType animType]
        {
            get { return uiAnimDir[animType]; }
        }

        public UiAnimFactory()
        {
            uiAnimDir = new Dictionary<UiAnimType, IUiAnim>();
            var uianimTypes = Enum.GetNames(typeof(UiAnimType));
            foreach (var value in uianimTypes)
            {
                Type type = Type.GetType("MiYue."+value);
               IUiAnim uiAnim = Activator.CreateInstance(type) as IUiAnim;
               uiAnimDir.Add((UiAnimType)Enum.Parse(typeof(UiAnimType),value),uiAnim);
            }
        }
    }

    public enum UiAnimType
    {
        AcrossIn, //淡入
        AcrossOut, //淡出
    }
}