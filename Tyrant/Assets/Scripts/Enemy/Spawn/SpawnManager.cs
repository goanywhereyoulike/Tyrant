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
    int roomCount = 1;
    bool roomClear = false;

    public int waveNumber;
    public float waveDelay;
    public bool waveDelayTurnOn;

    public bool RoomClear { get => roomClear; set => roomClear = value; }
    public int RoomCount { get => roomCount; set => roomCount = value; }

    //int randomSpawn;

    private void Start()
    {
        ObjectPoolManager.Instance.InstantiateObjects("normalenemy");
        spawns = new List<SpawnArea>();
        enemies = new List<GameObject>();
        //StartCoroutine(Spawn());
    }

    void Update()
    {
        if (spawns.Count == 0)
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

        CheckRoomClear();
        if (delayTime <= 0.0f)
        {
            Spawn();
            delayTime = waveDelay;
            return;
        }
        delayTime -= Time.deltaTime;

    }

    void CheckRoomClear()
    {
        if(RoomClear)
        {
            foreach (var spawn in spawns)
            {
                if(spawn.roomNumber== RoomCount)
                {
                    spawn.IsTurnOff = true;
                    spawns.Remove(spawn);
                }
            }
            RoomCount++;
            currentWave = 0;
            roomClear = false;
        }    
    }

    void GetSpawnArea()
    {
        var spawnArea = GameObjectsLocator.Instance.Get<SpawnArea>();
        foreach (var spawn in spawnArea)
        {
            if (spawn.roomNumber == RoomCount)
            {
                spawns.Add(spawn);
            }
        }
    }

    void Spawn()
    {
        if (waveDelayTurnOn)
        {
            //yield return new WaitForSeconds(waveDelay);
            if (currentWave < waveNumber)
            {
                Debug.Log(currentWave);
                SpawnWave(currentWave++);
                //yield return new WaitForSeconds(waveDelay);
            }
        }
        else
        {
            if (spawnCount == 0)
            {
                SpawnWave(currentWave++);
            }
        }
        
    }

    void SpawnWave(int count)
    {
       // yield return new WaitForSeconds(spawnDelay);
        for (int i = 0; i < spawns.Count; ++i)
        {
            //Debug.Log(spawns.Count);
            int c;
            for (c = 0; c < spawns[i].spawnNumber; ++c)
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
                //yield return new WaitForSeconds(spawnDelay);
            }
            if (c == spawns[i].spawnNumber)
            {
                c = 0;
            }
        }
    }
}
