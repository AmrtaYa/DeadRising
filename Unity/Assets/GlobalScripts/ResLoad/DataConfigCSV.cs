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
   public class DataConfigCSV : ConfigCSV<DataConfigCSV>
    {
        public Dictionary<UnitType, Dictionary<CareerType,UnitData> > unitDataConfig = new Dictionary<UnitType, Dictionary<CareerType,UnitData>>();
        private UnitType currentUnitType;
       public DataConfigCSV()
       {
           Init("Config/UnitDataConfig.csv",TextHandler);
       }

       public UnitData this[UnitType ut,CareerType ct]
       {
           get { return unitDataConfig[ut][ct]; }
       }

       private void TextHandler(string line)
       {
           if(line.Contains("Data")) return;
           if(string.IsNullOrEmpty(line.Replace(",",""))) return;
           if (line.Contains("[") || line.Contains("]"))
           {
               currentUnitType = (UnitType)Enum.Parse(typeof(UnitType), line.Replace(",","").Replace("[", "").Replace("]", ""));
               unitDataConfig.Add(currentUnitType,new Dictionary<CareerType, UnitData>());
           }
           else
           {
               var strArr = line.Split(',');
               var thisCareerType = (CareerType)Enum.Parse(typeof(CareerType), strArr[0]);
               unitDataConfig[currentUnitType].Add(thisCareerType, new UnitData()
               {
                   unitType = currentUnitType,
                   careerType = thisCareerType,
                   MaxHP = float.Parse( strArr[1]),
                   ATK = float.Parse( strArr[2]),
                   Speed = float.Parse( strArr[3]),
                   atkInterval = float.Parse( strArr[4]),
                   AttackRange = float.Parse( strArr[5]),
                   FindRange = float.Parse( strArr[6]),
               });
               
           }
       }
   }
}
