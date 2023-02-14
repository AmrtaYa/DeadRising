using System.Collections;
using System.Collections.Generic;
using GJC.Helper;
using UnityEngine;
namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
   public class CoroutineManager : SingleTon<CoroutineManager>
   {
       public Coroutine StartCor(IEnumerator enumerator)
       {
            return this.StartCoroutine(enumerator);
       }

       public void StopCor(Coroutine coroutine)
       {
           this.StopCoroutine(coroutine);
       }
   }
}
