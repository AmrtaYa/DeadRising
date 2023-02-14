using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
    public class FSMConfigCSV : ConfigCSV<FSMConfigCSV>
    {
        public Dictionary<string, Dictionary<string, string>> FsmConfig;
        public Dictionary<string, Dictionary<string, string>> DenfenceFsmConfig;
        public Dictionary<string, Dictionary<string, string>>TowerFsmConfig;
        public FSMConfigCSV()
        {
            FsmConfig = new Dictionary<string, Dictionary<string, string>>();
            DenfenceFsmConfig = new Dictionary<string, Dictionary<string, string>>();
            TowerFsmConfig = new Dictionary<string, Dictionary<string, string>>();
            Init("Config/FSMConfig.csv", (line) => { FsmConfigHandler(FsmConfig,line);});
            Init("Config/DenfenceFsmConfig.csv",(line) => { FsmConfigHandler(DenfenceFsmConfig,line);});
            Init("Config/TowerFsmConfig.csv",(line) => { FsmConfigHandler(TowerFsmConfig,line);});
        }
        private  string index;
        private void FsmConfigHandler(Dictionary<string, Dictionary<string, string>> config,string line)//默认状态请在各自的子类中加
        {
            line = line.Replace(",", "");
            if (line.Contains("["))
            {
                index = line.Replace("[", "").Replace("]", "");
                if (!config.ContainsKey(index))
                    config.Add(index, new Dictionary<string, string>());
            }
            else if (line.Contains(">"))
            {
                var lines = line.Split('>');//0是条件  1是状态
                config[index].Add(lines[0], lines[1]);
            }
        }
    }
}