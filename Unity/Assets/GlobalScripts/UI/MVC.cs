using System;
using System.Collections.Generic;
using MiYue;
using UnityEngine;

namespace Global
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Control: MonoBehaviour
    {

        public bool Open = false;
        protected virtual void OnEnable()
        {
        }

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