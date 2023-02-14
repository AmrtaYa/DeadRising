using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GJC.Helper;
using UnityEngine;
using UnityEngine.AI;

namespace MiYue
{
    /// <summary>
    /// 配置表读取   一般情况：第一列 资源名称  第二列 资源路径  本行带有Split跳过，不读取本行
    /// 资源名字请和数据库中一致
    /// 注意：用到gb2312的编码格式，请在Editor\Data\MonoBleedingEdge\lib\mono\unityjit-win32 下，把I18N和I18N.CJK加入到Asset文件下，否则打包后无法读取数据
    /// </summary>
    public class ConfigCSV<T> : SharpSington<T> where T : new()
    {
        private Dictionary<string, string> ResPath = new Dictionary<string, string>();

        /// <summary>
        /// 在子类调用此方法，路径格式是 "Config/FSMConfig.csv"
        /// </summary>
        /// <param name="resPath"></param>
        public void Init(string resPath, Action<string> handler = null)
        {
            TextAsset textAsset = Resources.Load<TextAsset>(resPath);
            using (StreamReader streamReader =
                   new StreamReader(new MemoryStream(textAsset.bytes), Encoding.GetEncoding("gb2312")))
            {
                string readContent;
                int line = -1;
                while ((readContent = streamReader.ReadLine()) != null)
                {
                    line++;
                    if (readContent.Replace(",","") == String.Empty) continue;
                    if (handler != null)
                        handler(readContent);
                    else
                    {
                        var strArr = readContent.Split(',');
                        if (readContent.Contains("Split"))
                            continue;
                        ResPath.Add(strArr[0], strArr[1]);
                    }
                }
            }
        }

        public string GetPath(string prefabName)
        {
            if(!ResPath.ContainsKey(prefabName)) Debug.LogError("资源不存在，请检查ResConfig中是否存在"+prefabName+ "资源！");
            return ResPath[prefabName];
        }

        public GameObject GetInstance(string prefabName)
        {
            var prefab = GetPrefab(prefabName);
            return GameObject.Instantiate(prefab);
        }

        public GameObject GetPrefab(string prefabName)
        {
            return Resources.Load<GameObject>(GetPath(prefabName));
        }

        public GameObject GetInstanceWithPool(string prefabName, Vector3 vector3, Quaternion quaternion)
        {
            GameObject init = GameObjectPool.Instance.Get(prefabName, GetPrefab(prefabName), vector3, quaternion);
            var nav = init.GetComponent<NavMeshAgent>();
            if ((nav) != null)
                nav.Warp(vector3);
            return init;
        }

        public void ReleaseInstance(GameObject gameObject)
        {
            GameObject.Destroy(gameObject);
        }

        public void ReleaseInstanceWithPool(GameObject gameObject)
        {
            GameObjectPool.Instance.Release(gameObject);
        }
    }
}