using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
    public class UIManager : SingleTon<UIManager>
    {
        private CanvasScaler Scaler;
        public override void init()
        {
            base.init();
            Scaler = GetComponent<CanvasScaler>();
        //  Scaler.referenceResolution = new Vector2(Screen.width,Screen.height);
        }

        public GameObject AddNewUI(string UIName, Vector3 vector3 = default)
        {
            GameObject go = ResConfigCSV.Instance.GetInstance(UIName);
            if (vector3 != default)
                go.transform.localPosition = vector3;
            go.transform.SetParent(transform);
            return go;
        }
        public GameObject AddNewUIWithPool(string UIName, Vector3 vector3 = default)
        {
            GameObject go = GameObjectPool.Instance.Get(UIName, ResConfigCSV.Instance.GetPrefab(UIName), Vector3.zero,
                Quaternion.identity);
            if (vector3 != default)
                go.transform.localPosition = vector3;
            go.transform.SetParent(transform);
            return go;
        }
    }
}