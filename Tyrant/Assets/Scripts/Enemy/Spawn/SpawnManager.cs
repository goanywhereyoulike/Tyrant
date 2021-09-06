using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    List<GameObject> enemies;

    [System.Serializable]
    public class room
    {
        public List<SpawnArea> roomSpawns;
    }
    public List<room> rooms = new List<room>();

    private room currentRoom;

    //[SerializeField]
    //GameObject Spawnportal;

    //List<GameObject> Portals;

    private static SpawnManager _instance;
    public static SpawnManager Instance { get => _instance; }
    public bool RoomClear { get => roomClear; set => roomClear = value; }

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
    private bool roomClear;
    private bool isWaveSpawn;
    private bool isRoomCheck;
    private int checkcount;
    private int lastroom;
    private int enemyDrop;
    //int randomSpawn;

    private void Start()
    {
        ObjectPoolManager.Instance.InstantiateObjects("normalenemy");
        ObjectPoolManager.Instance.InstantiateObjects("rangeEnemy");
        ObjectPoolManager.Instance.InstantiateObjects("Level1Boss");
        ObjectPoolManager.Instance.InstantiateObjects("DropItem");
        enemies = new List<GameObject>();
        RoomManager.Instance.RoomChanged += RoomChange;
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
        // check all spawns in current room
        if (currentRoom.roomSpawns.Count != 0)
        {
            for (int i = 0; i < currentRoom.roomSpawns.Count; ++i)
            {
                if (currentRoom.roomSpawns[i].CurrentWave == currentRoom.roomSpawns[i].Wave.waveData.Count)
                {
                    //currentRoom.roomSpawns[i].gameObject.SetActive(false);
                }

                if (currentRoom.roomSpawns[i].WaveDelayTurnOn)
                {
                    if (currentRoom.roomSpawns[i].DelayTime <= 0.0f)
                    {
                        SpawnWave(i);

                        currentWave++;
                        currentRoom.roomSpawns[i].DelayTime = currentRoom.roomSpawns[i].WaveDelay;
                    }
                    currentRoom.roomSpawns[i].DelayTime -= Time.deltaTime;
                }
                else
                {
                    SpawnWave(i);
                }

                //check how many enemy can drop item
                for (int d = currentRoom.roomSpawns[i].ChooseEnemyDrop; d < currentRoom.roomSpawns[i].ItemDropNum;)
                {
                    enemyDrop = Random.Range(0, currentRoom.roomSpawns[i].TotalEnemies);
                    if (currentRoom.roomSpawns[i].ItemDropNum <= currentRoom.roomSpawns[i].TotalEnemies)
                    {
                        while (currentRoom.roomSpawns[i].DropNumber.Contains(enemyDrop))
                        {
                            enemyDrop = Random.Range(0, currentRoom.roomSpawns[i].TotalEnemies);
                        }
                        currentRoom.roomSpawns[i].DropNumber.Add(enemyDrop);
                        currentRoom.roomSpawns[i].ChooseEnemyDrop++;
                    }
                    else
                    {
                        Debug.LogError("Drop number can not larger than enemy total number Room: " + i.ToString() + " Total Spawn Number: " + currentRoom.roomSpawns[i].TotalEnemies.ToString());
                    }
                    d++;
                }


                for (int e = 0; e < currentRoom.roomSpawns[i].Enemies.Count; ++e)
                {
                    if (currentRoom.roomSpawns[i].Enemies[e].activeInHierarchy == false)
                    {
                        //check which enemy will drop
                        for (int d = 0; d < currentRoom.roomSpawns[i].DropNumber.Count; ++d)
                        {
                            if (e == currentRoom.roomSpawns[i].DropNumber[d])
                            {
                                enemyObject = ObjectPoolManager.Instance.GetPooledObject("DropItem");
                                enemyObject.transform.position = currentRoom.roomSpawns[i].Enemies[e].transform.position;
                                enemyObject.SetActive(true);
                                currentRoom.roomSpawns[i].DropNumber.RemoveAt(d);
                                currentRoom.roomSpawns[i].ItemDropCount++;
                                break;
                            }
                        }

                        if (currentRoom.roomSpawns[i].ItemDropCount == currentRoom.roomSpawns[i].ItemDropNum)
                        {
                            currentRoom.roomSpawns[i].Enemies.RemoveAt(e);
                        }

                        currentRoom.roomSpawns[i].SpawnCount--;
                    }
                }
            }
        }


    }

    private void RoomChange(int changeId)
    {
        roomNumber = changeId;
        roomClear = false;
        if (roomNumber != lastroom)
        {
            lastroom = roomNumber;
            checkcount = 0;
        }
    }

    void CheckRoomSpawn()
    {
        currentRoom = rooms[roomNumber];
        foreach (var spawn in currentRoom.roomSpawns)
        {
            spawn.gameObject.SetActive(true);
        }

        for (int i = 0; i < currentRoom.roomSpawns.Count; ++i)
        {
            for (int s = 0; s < currentRoom.roomSpawns[i].Wave.waveData.Count; ++s)
            {
                currentRoom.roomSpawns[i].TotalEnemies += currentRoom.roomSpawns[i].Wave.waveData[s].spawnNumber;
            }
        }
        isRoomCheck = true;
    }

    void CheckRoomClear()
    {
        if (currentRoom.roomSpawns.Count != 0)
        {
            for (int i = 0; i < currentRoom.roomSpawns.Count; ++i)
            {
                if (currentRoom.roomSpawns[i].CurrentWave == currentRoom.roomSpawns[i].Wave.waveData.Count && currentRoom.roomSpawns[i].SpawnCount == 0 && !currentRoom.roomSpawns[i].SpawnClear)
                {
                    currentRoom.roomSpawns[i].SpawnClear = true;
                    checkcount++;
                }

                if (checkcount == currentRoom.roomSpawns.Count)
                {
                    if (currentRoom.roomSpawns[i].Enemies.Count == 0)
                    {
                        isRoomCheck = false;
                        RoomClear = true;
                        //foreach (var portal in Portals)
                        //{
                        //    Destroy(portal);
                        //}
                        //Portals.Clear();
                        break;
                    }
                }
            }
        }
    }

    void SpawnWave(int count)
    {
        int c;
        if (currentRoom.roomSpawns[count].WaveDelayTurnOn)
        {
            if (currentRoom.roomSpawns[count].CurrentWave < currentRoom.roomSpawns[count].Wave.waveData.Count)
            {
                for (c = 0; c < currentRoom.roomSpawns[count].Wave.waveData[currentRoom.roomSpawns[count].CurrentWave].spawnNumber; ++c)
                {
                    isWaveSpawn = true;
                    switch (currentRoom.roomSpawns[count].Wave.waveData[currentRoom.roomSpawns[count].CurrentWave].enemytype)
                    {
                        case Wave.Enemytype.normalenemy:
                            spawnMax = new Vector2(currentRoom.roomSpawns[count].SpMax.x, currentRoom.roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(currentRoom.roomSpawns[count].SpMin.x, currentRoom.roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)currentRoom.roomSpawns[count].SpMin.x, (int)currentRoom.roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)currentRoom.roomSpawns[count].SpMin.y, (int)currentRoom.roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("normalenemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            currentRoom.roomSpawns[count].SpawnCount++;
                            currentRoom.roomSpawns[count].Enemies.Add(enemyObject);
                            print("spawn");
                            break;
                        case Wave.Enemytype.rangeEnemy:
                            spawnMax = new Vector2(currentRoom.roomSpawns[count].SpMax.x, currentRoom.roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(currentRoom.roomSpawns[count].SpMin.x, currentRoom.roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)currentRoom.roomSpawns[count].SpMin.x, (int)currentRoom.roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)currentRoom.roomSpawns[count].SpMin.y, (int)currentRoom.roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("rangeEnemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            currentRoom.roomSpawns[count].Enemies.Add(enemyObject);
                            currentRoom.roomSpawns[count].SpawnCount++;
                            print("spawn");
                            break;
                        case Wave.Enemytype.Level1Boss:
                            spawnMax = new Vector2(currentRoom.roomSpawns[count].SpMax.x, currentRoom.roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(currentRoom.roomSpawns[count].SpMin.x, currentRoom.roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)currentRoom.roomSpawns[count].SpMin.x, (int)currentRoom.roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)currentRoom.roomSpawns[count].SpMin.y, (int)currentRoom.roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("Level1Boss");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            currentRoom.roomSpawns[count].Enemies.Add(enemyObject);
                            currentRoom.roomSpawns[count].SpawnCount++;
                            print("spawn");
                            break;
                        default:
                            break;
                    }
                    //spawn position


                }
                //if (c == roomSpawns[count].Wave.waveData[c].spawnNumber)
                //{
                //    c = 0;
                //}
                if (isWaveSpawn)
                {
                    currentRoom.roomSpawns[count].CurrentWave++;
                    isWaveSpawn = false;
                }
            }
        }
        else
        {
            if (currentRoom.roomSpawns[count].SpawnCount == 0 && currentRoom.roomSpawns[count].CurrentWave < currentRoom.roomSpawns[count].Wave.waveData.Count)
            {
                for (c = 0; c < currentRoom.roomSpawns[count].Wave.waveData[currentRoom.roomSpawns[count].CurrentWave].spawnNumber; ++c)
                {
                    isWaveSpawn = true;
                    //spawn position
                    switch (currentRoom.roomSpawns[count].Wave.waveData[currentRoom.roomSpawns[count].CurrentWave].enemytype)
                    {
                        case Wave.Enemytype.normalenemy:
                            spawnMax = new Vector2(currentRoom.roomSpawns[count].SpMax.x, currentRoom.roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(currentRoom.roomSpawns[count].SpMin.x, currentRoom.roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)currentRoom.roomSpawns[count].SpMin.x, (int)currentRoom.roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)currentRoom.roomSpawns[count].SpMin.y, (int)currentRoom.roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("normalenemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            currentRoom.roomSpawns[count].SpawnCount++;
                            currentRoom.roomSpawns[count].Enemies.Add(enemyObject);
                            print("spawn");
                            break;
                        case Wave.Enemytype.rangeEnemy:
                            spawnMax = new Vector2(currentRoom.roomSpawns[count].SpMax.x, currentRoom.roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(currentRoom.roomSpawns[count].SpMin.x, currentRoom.roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)currentRoom.roomSpawns[count].SpMin.x, (int)currentRoom.roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)currentRoom.roomSpawns[count].SpMin.y, (int)currentRoom.roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("rangeEnemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            currentRoom.roomSpawns[count].Enemies.Add(enemyObject);
                            currentRoom.roomSpawns[count].SpawnCount++;
                            print("spawn");
                            break;
                        case Wave.Enemytype.Level1Boss:
                            spawnMax = new Vector2(currentRoom.roomSpawns[count].SpMax.x, currentRoom.roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(currentRoom.roomSpawns[count].SpMin.x, currentRoom.roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)currentRoom.roomSpawns[count].SpMin.x, (int)currentRoom.roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)currentRoom.roomSpawns[count].SpMin.y, (int)currentRoom.roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("Level1Boss");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            currentRoom.roomSpawns[count].Enemies.Add(enemyObject);
                            currentRoom.roomSpawns[count].SpawnCount++;
                            print("spawn");
                            break;
                        default:
                            break;
                    }
                }

                if (isWaveSpawn && currentRoom.roomSpawns[count].SpawnCount!=0)
                {
                    currentRoom.roomSpawns[count].CurrentWave++;
                    isWaveSpawn = false;
                }
            }
        }
    }
}
