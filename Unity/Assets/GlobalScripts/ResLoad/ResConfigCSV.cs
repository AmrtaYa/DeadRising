using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
   public class ResConfigCSV : ConfigCSV<ResConfigCSV>
   {
       public ResConfigCSV()
       {
           Init("Config/ResConfig.csv");
       }
   }
}
