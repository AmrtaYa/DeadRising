using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GJC.Helper
{
    /*
     ʹ�÷�ʽ��
    1.Get(���ö�������ƣ����ɵ�Ԥ�Ƽ���λ�ã���ת) �����Ҫ�ڻ�ȡ��ʱ�����������Ϊ����ʵ�ִ������ռ��е�Ireset�ӿ�
    2��Release() �Żض���أ��Զ�setactiveΪfalse����������ͷ�ʱ�����������Ϊ����ʵ�ִ������ռ��е�Irelease�ӿ�
    3��PoolDestroy() ����  �ж�����أ����Ը��ݵ������壬������������Ƶ�ȫ�����ֻ�����ȫ��
     */

    /// <summary>
    /// ��Ϸ�����  unity�Դ�������������Լ�д��
    /// ��Ҫ����GJC.Helper�е�SingleTon�����ű�
    /// </summary>
    public class GameObjectPool : SingleTon<GameObjectPool>
    {
        public Dictionary<string, List<GameObject>> pool;
        public override void init()
        {
            base.init();
            pool = new Dictionary<string, List<GameObject>>();
        }
        public GameObject Get(string poolDir, GameObject go, Vector3 pos, Quaternion rot)
        {
            GameObject index = CreateGameObject(poolDir, go);
            index.SetActive(true);
            index.transform.position = pos;
            index.transform.rotation = rot;
            var inter = index.GetComponents<IRsetable>();
            if (inter != null)
                foreach (var item in inter)
                {
                    item.OnRest();
                }
            return index;
        }
        private GameObject CreateGameObject(string poolDir, GameObject go)
        {
            if (!pool.ContainsKey(poolDir))
            {
                pool.Add(poolDir, new List<GameObject>());
            }
            GameObject index = pool[poolDir].Find(G => !G.activeInHierarchy);
            if (index == null)
            {
                index = Instantiate<GameObject>(go);
                pool[poolDir].Add(index);
            }

            return index;
        }
        private IEnumerator ReleaseIE(GameObject go, float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            if (go != null)
            {
                var inter = go.GetComponents<IRelease>();
                if (inter != null)
                    foreach (var item in inter)
                    {
                        item.OnRelease();
                    }
                go.SetActive(false);
            }


        }
        public void Release(GameObject go, float delay = 0)
        {
            StartCoroutine(ReleaseIE(go, delay));
        }


        private IEnumerator DestroyIE(GameObject go, float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            GameObject.Destroy(go);
        }
        /// <summary>
        /// ���ٸ�key�е�����Ԫ��
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        private IEnumerator DestroyIE(string key, float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            foreach (var item in pool[key])
            {
                GameObject.Destroy(item);
            }
            pool.Remove(key);
        }
        /// <summary>
        /// ��������Ԫ��
        /// </summary>
        /// <param name="delay"></param>
        /// <returns></returns>
        private IEnumerator DestroyIE(float delay = 0)
        {
            yield return new WaitForSeconds(delay);
            List<string> index = new List<string>(pool.Keys);
            foreach (var item in index)
            {
                StartCoroutine(DestroyIE(item));
            }

        }


        /// <summary>
        /// �����ֵ����ض�������
        /// </summary>
        /// <param name="go"></param>
        /// <param name="delay"></param>
        public void PoolDestroy(GameObject go, float delay = 0)
        {
            StartCoroutine(DestroyIE(go, delay));
        }
        /// <summary>
        /// ����һ��key�������Ԫ��
        /// </summary>
        /// <param name="key"></param>
        /// <param name="delay"></param>
        public void PoolDestroy(string key, float delay = 0)
        {
            StartCoroutine(DestroyIE(key, delay));
        }
        /// <summary>
        /// ����ȫ��
        /// </summary>
        /// <param name="delay"></param>
        public void PoolDestroy(float delay = 0)
        {
            StartCoroutine(DestroyIE(delay));
        }
    }
    public interface IRelease
    { void OnRelease(); }
    public interface IRsetable
    { void OnRest(); }

}

