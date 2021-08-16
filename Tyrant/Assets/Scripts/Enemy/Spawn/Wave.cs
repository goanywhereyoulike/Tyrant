using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "SpawnWaveData", menuName = "SpawnWave")]
public class Wave : ScriptableObject
{
    public enum Enemytype
    {
        normalenemy, rangeEnemy
    }

    [Serializable]
    public class Wavedata
    {
        public Enemytype enemytype = 0;
    }

    public List<Wavedata> waveData = new List<Wavedata>();
}
