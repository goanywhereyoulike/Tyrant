using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    List<GameObject> enemies;
    List<SpawnArea> spawns;

    GameObject enemyObject;

    Vector2 spawnPosition;
    Vector2 spawnMax;
    Vector2 spawnMin;

    float delayTime;
    int spawnX;
    int spawnY;
    int spawnCount;
    int currentWave = 0;
    int roomNumber = 0;


    string basicName;
    public int waveNumber;
    public float waveDelay;
    public bool waveDelayTurnOn;

    //int randomSpawn;

    private void Start()
    {
        ObjectPoolManager.Instance.InstantiateObjects("normalenemy");
        ObjectPoolManager.Instance.InstantiateObjects("rangeEnemy");
        spawns = new List<SpawnArea>();
        enemies = new List<GameObject>();
        RoomManager.Instance.RoomChanged = RoomChange;
        //StartCoroutine(Spawn());
       
    }

    void Update()
    {
        if(currentWave == 0)
        {
            GetSpawnArea();
        }

        for (int i = 0; i < enemies.Count; ++i)
        {
            if (enemies[i].activeInHierarchy == false)
            {
                enemies.RemoveAt(i);
                spawnCount--;
            }
        }
        if (waveDelayTurnOn)
        {
            if (delayTime <= 0.0f)
            {
                Spawn();
                currentWave++;
                delayTime = waveDelay;
            }
            delayTime -= Time.deltaTime;
        }
        else
        {
            Spawn();
        }

    }

    private void RoomChange(int changeId)
    {
        currentWave = 0;
        roomNumber = changeId;
        foreach (var spawn in spawns)
        {
            if (spawn.roomNumber < changeId)
            {
                spawn.UnRegisterToLocator();
            }
        }
    }

    void GetSpawnArea()
    {
        spawns.Clear();
        var spawnArea = GameObjectsLocator.Instance.Get<SpawnArea>();
        foreach (var spawn in spawnArea)
        {
           spawns.Add(spawn);
        }
    }

    void Spawn()
    {
        if (waveDelayTurnOn)
        {
            if (currentWave < waveNumber)
            {
                Debug.Log(currentWave);
                SpawnWave();
            }
        }
        else
        {
            if (spawnCount == 0 && currentWave < waveNumber)
            {
                SpawnWave();
                currentWave++;
            }
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < spawns.Count; ++i)
        {
            int c;
            for (c = 0; c < spawns[i].spawnNumber; ++c)
            {
                if (spawns[i].roomNumber == roomNumber)
                {
                    spawnMax = new Vector2(spawns[i].SpMax.x, spawns[i].SpMax.y);
                    spawnMin = new Vector2(spawns[i].SpMin.x, spawns[i].SpMin.y);
                    spawnX = Random.Range((int)spawns[i].SpMin.x, (int)spawns[i].SpMax.x);
                    spawnY = Random.Range((int)spawns[i].SpMin.y, (int)spawns[i].SpMax.y);
                    spawnPosition = new Vector2(spawnX, spawnY);

                    enemyObject = ObjectPoolManager.Instance.GetPooledObject(spawns[i].enemyType);
                    enemyObject.transform.position = spawnPosition;
                    enemyObject.SetActive(true);
                    enemies.Add(enemyObject);
                    spawnCount++;
                    print("spawn");
                }
            }
            if (c == spawns[i].spawnNumber)
            {
                c = 0;
            }
        }
    }
}
