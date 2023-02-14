using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace MiYue
{
    /// <summary>
    /// 升级系统
    /// </summary>
    public class LevelSystem : MonoBehaviour
    {
        public Unit unit;

        private void Update()
        {
            if (unit == null) return;
            UnitData unitData = unit.Data;
            if (unitData.MaxHP <= 0) return;
            unitData.currentEXP += Time.deltaTime;
            if (unitData.currentEXP >= unitData.UpLevelEXP)
            {
                Level();
                unitData.Level++;
                unitData.currentEXP = 0;
            }
        }

        private void Level()
        {
            UnitData levelData = LevelConfigCSV.Instance[unit.Data.unitType, unit.Data.careerType];
            unit.Data.MaxHP += levelData.MaxHP;
            unit.Data.HP = unit.Data.MaxHP;
            unit.Data.ATK += levelData.ATK;
            print($"升级了，增加了攻击力{levelData.ATK}和血量{levelData.MaxHP}");
        }
    }
}