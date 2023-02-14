using System;
using System.Collections;
using System.Collections.Generic;
using FSM;
using GJC.Helper;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace MiYue
{
    /// <summary>
    /// 
    /// </summary>
    public class SpawnManager : SingleTon<SpawnManager>
    {
        public Dictionary<SpawnType, List<Transform>> spawn;

        public override void init()
        {
            base.init();
            spawn = new Dictionary<SpawnType, List<Transform>>();
            foreach (var value in Enum.GetValues(typeof(SpawnType)))
            {
                spawn.Add((SpawnType)value, new List<Transform>());
                InitSpawn(((SpawnType)value).ToString());
            }
            foreach (var point in spawn[SpawnType.ZombieAttackSpawn])
            {
                point.gameObject.AddComponent<SmallWall>();
            }
        }

        public List<Transform> this[SpawnType spawnType]
        {
            get { return spawn[spawnType]; }
        }

        public Transform RandomGetSpawnPoint(SpawnType spawnType)
        {
            var arr = spawn[spawnType];
            int randInt = Random.Range(0, arr.Count);
            return arr[randInt];
        }
/// <summary>
/// 
/// </summary>
/// <param name="vector3">哪个点和Spawn数组中距离最短</param>
/// <param name="spawnType"></param>
/// <returns></returns>
        public Transform RandomGetMinPoint(Vector3 vector3, SpawnType spawnType)
        {
            var arr = spawn[spawnType];
           return arr.ToArray().GetMin(t => Vector3.Distance(t.position, vector3));
        }

        private void InitSpawn(string SpawnName)
        {
            GameObject attackSpawnGO = transform.FindTheTfByName(SpawnName).gameObject;
            for (int i = 0; i < attackSpawnGO.transform.childCount; i++)
            {
                spawn[(SpawnType)Enum.Parse(typeof(SpawnType), SpawnName)].Add(attackSpawnGO.transform.GetChild(i));
            }
        }
    }

    public enum SpawnType
    {
        DenfenceAttackSpawn,
        ZombieSpawn,
        DenfenceSpawn,
        ZombieAttackSpawn,
        WallSpawn,
        TowerSpawn
    }
}