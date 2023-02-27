using System;
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
        private Dictionary<UILayer, Transform> uiLayerDir;

        public override void init()
        {
            base.init();
            DontDestroyOnLoad(gameObject);
            Scaler = GetComponent<CanvasScaler>();
            //  Scaler.referenceResolution = new Vector2(Screen.width,Screen.height);
            uiLayerDir = new Dictionary<UILayer, Transform>();
            InitUILayer();
        }

        private void InitUILayer()
        {
            foreach (var value in Enum.GetNames(typeof(UILayer)))
            {
                UILayer uiLayer = (UILayer)Enum.Parse(typeof(UILayer), value);
                uiLayerDir.Add(uiLayer, transform.FindTheTfByName(value));
            }
        }

        public void ClearLayerUI()
        {
            foreach (Transform layer in uiLayerDir.Values)
            {
                for (int i = 0; i < layer.childCount; i++)
                {
                    var transform = layer.GetChild(i);
                    Destroy(transform.gameObject);
                }
            }
        }

        public void ClearLayerUI(UILayer uiLayer)
        {
            for (int i = 0; i < uiLayerDir[uiLayer].childCount; i++)
            {
                var transform = uiLayerDir[uiLayer].GetChild(i);
                Destroy(transform.gameObject);
            }
        }

        public GameObject AddNewUI(string UIName, UILayer layer = UILayer.Mid, Vector3 vector3 = default)
        {
            GameObject go = ResConfigCSV.Instance.GetInstance(UIName);
            if (vector3 != default)
                go.transform.localPosition = vector3;
            go.transform.SetParent(uiLayerDir[layer]);
            return go;
        }

        public GameObject AddNewUIWithPool(string UIName, UILayer layer = UILayer.Mid, Vector3 vector3 = default)
        {
            if (vector3 == default)
                vector3 = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            GameObject go = GameObjectPool.Instance.Get(UIName, ResConfigCSV.Instance.GetPrefab(UIName), Vector3.zero,
                Quaternion.identity);
            if (vector3 != default)
                go.transform.localPosition = vector3;
            go.transform.SetParent(uiLayerDir[layer]);
            return go;
        }
    }

    public enum UILayer
    {
        Normal,
        Mid,
        Top
    }
}