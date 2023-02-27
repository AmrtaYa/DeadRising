using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
   public class CameraCtrl : MonoBehaviour
    {
        private Camera Camera;
        public float sightSpeed = 1f;
        private void Start()
        {
            Camera = GetComponent<Camera>();
        }

        private void Update()
        {
            if (Camera == null) return;
          
            //视角 10 ~60
            CtrlSighting(10,60);
            //X -65~ -10 Z -50~25
            CtrlMove(new Vector3(-65,0,-50),new Vector3(-10,0,25));
        }

        private void CtrlMove(Vector3 range1,Vector3 range2)
        {
            float hor = Input.GetAxis("Horizontal");
            float vec = Input.GetAxis("Vertical");
            // if (transform.position.x < range1.x && hor < 0)
            // {
            //     print(1);
            // return;
            // }
            // if (transform.position.x < range1.x && vec< 0) 
            // {
            //     print(2);
            //     return;
            // }
            // if (transform.position.z > range2.z && hor > 0)
            // {
            //     print(3);
            //     return;
            // }
            // if (transform.position.z > range2.z && vec > 0) 
            // {
            //     print(4);
            //     return;
            // }
            transform.position += new Vector3(hor, 0, vec) ;
        }

        private void CtrlSighting(float min,float max)
        {
            float scrollAxis = Input.GetAxis("Mouse ScrollWheel");
            if (Camera.orthographicSize < min && scrollAxis < 0) return;
            if (Camera.orthographicSize >max && scrollAxis > 0) return;
            Camera.orthographicSize += Input.GetAxis("Mouse ScrollWheel") *sightSpeed;
        }
    }
}
