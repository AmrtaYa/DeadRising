using System;
using System.Collections;
using System.Collections.Generic;
using FSM;
using SQLite4Unity3d;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Serialization;

namespace MiYue
{
    /// <summary>
    /// 单位基类
    /// </summary>
    [RequireComponent(typeof(LevelSystem))]
    public abstract class Unit : MonoBehaviour
    {
        protected virtual void Awake()
        {
            levelSystem = GetComponent<LevelSystem>();
            levelSystem.unit = this;
            MoneyManager = new SinglePlayerMoneyManager(this);
        }

        public UnitData Data;
        public LevelSystem levelSystem;
        public SinglePlayerMoneyManager MoneyManager;
        public abstract void Attack();
        public abstract void Dead();
        public abstract void Damage(float atkDamage);
    }

    [Serializable]
    public class UnitData
    {
        public UnitData()
        {
        }

        public UnitData(UnitData data)
        {
            HP = data.HP;
            MaxHP = data.MaxHP;
            Level = data.Level;
            ATK = data.ATK;
            Speed = data.Speed;
            atkInterval = data.atkInterval;
            unitType = data.unitType;
            AttackRange = data.AttackRange;
            FindRange = data.FindRange;
            careerType = data.careerType;
            PlayerName = data.PlayerName;
        }

        [PrimaryKey] public int UID { get; set; }
        public float HP;
        public float MaxHP;
        public float Level = 1;
        public float ATK;
        public float Speed;
        public float atkInterval; //攻击间隔
        public UnitType unitType; //兵种类型
        public float AttackRange; //攻击范围
        public float FindRange; //发现敌人范围  进入追逐
        public CareerType careerType;
        public string PlayerName { get; set; }
        public Texture2D PlayerTexture;
        public float currentEXP;

        public float UpLevelEXP
        {
            get { return Level * Level * GameGlobalSetting.LevelEXP; }
        }
    }

    public enum CareerType
    {
        Denfence = 0,
        Zombie = 1,
        Tower = 2,
        CanDestoryWall = 3,

        Alien = 100,
    }

    [Flags]
    public enum UnitType
    {
        Denfence = 1,
        Zombie = 2,
        Tower = 4,
        CanDestoryWall = 8,
    }
}