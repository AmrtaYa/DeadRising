using System;
using UnityEngine;

namespace Global
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Control: MonoBehaviour
    {
        private void Start()
        {
            Init();
        }

        protected abstract void Init();
    }

    public abstract class View
    {
    
    }

    public abstract class Model
    {
    
    }
}