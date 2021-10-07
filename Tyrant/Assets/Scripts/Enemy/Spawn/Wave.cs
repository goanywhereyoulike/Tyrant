using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnWaveData", menuName = "SpawnWave")]
public class Wave : ScriptableObject
{
    public enum Enemytype
    {
        normalenemy, rangeEnemy , Level1Boss , bombenemy, armorenemy, boomerangeenemy
    }

    [Serializable]
    public class Wavedata
    {
        public Enemytype enemytype = 0;
        public int spawnNumber = 0;
        //public float delaytime = 0f;
    }

    public List<Wavedata> waveData = new List<Wavedata>();
}
