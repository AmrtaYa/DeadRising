using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
   public class TestPackage : MonoBehaviour
    {
        private Text TEXT;
       private void Start()
       {
           TEXT = GetComponent<Text>();
           TextAsset textAsset = Resources.Load<TextAsset>("Config/UnitDataConfig.csv");
           StringBuilder STR = new StringBuilder();
           using (StreamReader streamReader =
                  new StreamReader(new MemoryStream(textAsset.bytes),Encoding.GetEncoding("gb2312")))//, Encoding.GetEncoding("gb2312")
           {
               string readContent;
               int line = -1;
               while ((readContent = streamReader.ReadLine()) != null)
               {
                   line++;
                   if (readContent.Replace(",","") == String.Empty) continue;
                   STR.Append(readContent);
               }
           }
           TEXT.text =STR.ToString();

       }
   }
}
