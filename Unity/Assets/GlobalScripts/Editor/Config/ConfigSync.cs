using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
namespace GJC.Editor
{
    /// <summary>
    /// 配置文件同步
    /// </summary>
   public class ConfigSync : UnityEditor.Editor
    {
        private static string OutConfigPath = Application .dataPath+"../../../Config";
        private static string InsideConfigPath = Application .dataPath+"/GlobalRes/Resources/Config/";
       [MenuItem("Tools/Config/配置同步")]
       public static void SyncConfig()
       {
           if (!Directory.Exists(InsideConfigPath))
           {
               Directory.CreateDirectory(InsideConfigPath);
           }

           if (!Directory.Exists(OutConfigPath))
           {
               Debug.LogError("需要在Unity工程上一级同文件内创建一个名为Config的文件夹");
           }

           DirectoryInfo infoDirOUT = new DirectoryInfo(OutConfigPath);
           DirectoryInfo infoDirIN = new DirectoryInfo(InsideConfigPath);
           var files = infoDirOUT.GetFiles();
           foreach (var file in files)
           {
              FileInfo newFile =  file.CopyTo(infoDirIN.FullName+file.Name+".bytes",true);
              Debug.Log(newFile.Name+"配置同步成功");
           }
       }
   }
}
