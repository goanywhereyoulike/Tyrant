using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    List<GameObject> enemies;
    List<SpawnArea> spawns;
    List<SpawnArea> roomSpawns;

    private static SpawnManager _instance;
    public static SpawnManager Instance { get => _instance; }

    GameObject enemyObject;

    Vector2 spawnPosition;
    Vector2 spawnMax;
    Vector2 spawnMin;

    int spawnX;
    int spawnY;
    int spawnCount;
    int currentWave = 0;
    int roomNumber = 0;

    string basicName;
    public bool waveDelayTurnOn;
    private bool roomClear;
    private bool isRoomCheck;
    private int checkcount;
    private int lastroom;
    //int randomSpawn;

    private void Start()
    {
        ObjectPoolManager.Instance.InstantiateObjects("normalenemy");
        ObjectPoolManager.Instance.InstantiateObjects("rangeEnemy");
        spawns = new List<SpawnArea>();
        enemies = new List<GameObject>();
        roomSpawns = new List<SpawnArea>();
        RoomManager.Instance.RoomChanged = RoomChange;
        //StartCoroutine(Spawn());
        GetSpawnArea();
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (!isRoomCheck)
            CheckRoomSpawn();

        CheckRoomClear();
        if (roomSpawns.Count != 0)
        {
            for (int i = 0; i < roomSpawns.Count; ++i)
            {
                if (waveDelayTurnOn)
                {
                    if (roomSpawns[i].DelayTime <= 0.0f)
                    {
                        SpawnWave(i);
                        currentWave++;
                        roomSpawns[i].DelayTime = roomSpawns[i].WaveDelay;
                    }
                    roomSpawns[i].DelayTime -= Time.deltaTime;
                }
                else
                {
                    SpawnWave(i);
                }


                for (int e = 0; e < roomSpawns[i].Enemies.Count; ++e)
                {
                    if (roomSpawns[i].Enemies[e].activeInHierarchy == false)
                    {
                        roomSpawns[i].Enemies.RemoveAt(e);
                        roomSpawns[i].SpawnCount--;
                    }
                }
            }
        }
    }

    private void RoomChange(int changeId)
    {
        roomNumber = changeId;
        checkcount = 0;
        if (roomNumber != lastroom)
        {
            lastroom = roomNumber;
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
    void CheckRoomSpawn()
    {
        foreach (var spawn in spawns)
        {
            if (spawn.roomNumber == roomNumber)
            {
                roomSpawns.Add(spawn);
            }
        }
        isRoomCheck = true;
    }

    void CheckRoomClear()
    {
        for (int i = 0; i < roomSpawns.Count; ++i)
        {
            if (roomSpawns[i].CurrentWave == roomSpawns[i].WaveNumber && roomSpawns[i].SpawnCount == 0 && !roomSpawns[i].SpawnClear)
            {
                roomSpawns[i].SpawnClear = true;
                checkcount++;
            }

            if (checkcount == roomSpawns.Count)
            {
                roomSpawns.Clear();
                isRoomCheck = false;
                roomClear = true;
                break;
            }
        }

    }

    void SpawnWave(int count)
    {
        int c;
        if (waveDelayTurnOn)
        {
            if (roomSpawns[count].CurrentWave < roomSpawns[count].WaveNumber)
            {
                for (c = 0; c < roomSpawns[count].spawnNumber; ++c)
                {
                    //spawn position
                    spawnMax = new Vector2(roomSpawns[count].SpMax.x, roomSpawns[count].SpMax.y);
                    spawnMin = new Vector2(roomSpawns[count].SpMin.x, roomSpawns[count].SpMin.y);
                    spawnX = Random.Range((int)roomSpawns[count].SpMin.x, (int)roomSpawns[count].SpMax.x);
                    spawnY = Random.Range((int)roomSpawns[count].SpMin.y, (int)roomSpawns[count].SpMax.y);
                    spawnPosition = new Vector2(spawnX, spawnY);

                    enemyObject = ObjectPoolManager.Instance.GetPooledObject("normalenemy");
                    enemyObject.transform.position = spawnPosition;
                    enemyObject.SetActive(true);
                    roomSpawns[count].Enemies.Add(enemyObject);
                    roomSpawns[count].SpawnCount++;
                    print("spawn");

                }
                if (c == roomSpawns[count].spawnNumber)
                {
                    c = 0;
                }
                roomSpawns[count].CurrentWave++;
            }
        }
        else
        {
            if (roomSpawns[count].SpawnCount == 0 && roomSpawns[count].CurrentWave < roomSpawns[count].WaveNumber)
            {
                for (c = 0; c < roomSpawns[count].spawnNumber; ++c)
                {
                    //spawn position
                    spawnMax = new Vector2(roomSpawns[count].SpMax.x, roomSpawns[count].SpMax.y);
                    spawnMin = new Vector2(roomSpawns[count].SpMin.x, roomSpawns[count].SpMin.y);
                    spawnX = Random.Range((int)roomSpawns[count].SpMin.x, (int)roomSpawns[count].SpMax.x);
                    spawnY = Random.Range((int)roomSpawns[count].SpMin.y, (int)roomSpawns[count].SpMax.y);
                    spawnPosition = new Vector2(spawnX, spawnY);

                    enemyObject = ObjectPoolManager.Instance.GetPooledObject(roomSpawns[count].EnemyType);
                    enemyObject.transform.position = spawnPosition;
                    enemyObject.SetActive(true);
                    roomSpawns[count].Enemies.Add(enemyObject);
                    roomSpawns[count].SpawnCount++;
                    print("spawn");
                }
                if (c == roomSpawns[count].spawnNumber)
                {
                    c = 0;
                }
                roomSpawns[count].CurrentWave++;
            }
        }
    }
}
