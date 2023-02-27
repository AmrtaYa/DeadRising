using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using Global;
using UnityEngine;
using UnityEngine.UI;

namespace MiYue
{
    public class AcrossIn : IUiAnim
    {
        private float speed = 2f;
        public void PlayAnim<T>(Control ctrl) where T : MaskableGraphic
        {
            T graphic = ctrl.transform.GetComponent<T>();
            if (graphic==null)
            {
                graphic = ctrl.transform.GetComponentInChildren<T>();
            }
            CoroutineManager.Instance.StartCor(AcrossInProcess(graphic,ctrl));
        }

        private IEnumerator AcrossInProcess(MaskableGraphic graphic,Control ctrl)
        {
            ctrl.Open = true;
               Color color = new Color( graphic.color.r,graphic.color.g,graphic.color.b,graphic.color.a);
               graphic.color = new Color(color.r, color.g, color.b,0F);
            while (graphic.color.a <=1)
            {
                graphic.color +=new Color(0,0,0,speed*(1f / 255F));
                if (!ctrl.Open)
                {
                    yield break;
                }
                yield return new WaitForFixedUpdate();
            }
        }
    }

    public class AcrossOut : IUiAnim
    {
      private  float speed = 2f;
        private IEnumerator AcrossOutProcess(MaskableGraphic graphic,Control ctrl)
        {
            ctrl.Open = false;
            Color color = new Color( graphic.color.r,graphic.color.g,graphic.color.b,graphic.color.a);
            graphic.color = new Color(color.r, color.g, color.b,color.a);
            while (graphic.color.a >0)
            {
                graphic.color -=new Color(0,0,0,speed*(1f / 255F));
                if (ctrl.Open)
                {
                    yield break;
                }
                yield return new WaitForFixedUpdate();
            }
           GameObjectPool.Instance.Release(graphic.gameObject);
        }
        public void PlayAnim<T>(Control ctrl) where T : MaskableGraphic
        {
            T graphic = ctrl.transform.GetComponent<T>();
            if (graphic==null)
            {
                graphic = ctrl.transform.GetComponentInChildren<T>();
            }
            CoroutineManager.Instance.StartCor(AcrossOutProcess(graphic,ctrl));
        }
    }
}