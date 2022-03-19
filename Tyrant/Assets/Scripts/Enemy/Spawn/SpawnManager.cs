using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    List<GameObject> enemies;

    [System.Serializable]
    public class room
    {
        public List<SpawnArea> roomSpawns;
        public bool clear;
        public bool isBossRoom;
        public bool bossSkillOver;
    }
    public List<room> rooms = new List<room>();

    private room currentRoom;
    private static SpawnManager _instance;

    public static SpawnManager Instance { get => _instance; }
    public bool RoomClear { get => roomClear; set => roomClear = value; }
    public bool StartLevel { get => startLevel; set => startLevel = value; }

    GameObject enemyObject;
    GameObject coin;

    Vector2 spawnPosition;
    Vector2 spawnMax;
    Vector2 spawnMin;

    int spawnX;
    int spawnY;
    int spawnCount;
    //int currentWave = 0;
    int roomNumber = 0;

    private bool roomClear;
    private bool isWaveClear;
    private bool isRoomCheck;
    private bool startLevel;
    private int checkcount;
    private int lastroom;
    private int enemyDrop;
    private bool canSpawnWave = true;
    private bool firstSpawn = true;

    [SerializeField]
    private int firstDelayTime;
    private float time;

    //int randomSpawn;

    private void Start()
    {
        ObjectPoolManager.Instance.InstantiateObjects("normalenemy");
        ObjectPoolManager.Instance.InstantiateObjects("rangeEnemy");
        ObjectPoolManager.Instance.InstantiateObjects("Level1Boss");
        ObjectPoolManager.Instance.InstantiateObjects("bombenemy");
        ObjectPoolManager.Instance.InstantiateObjects("armorenemy");
        ObjectPoolManager.Instance.InstantiateObjects("boomerangeenemy");
        ObjectPoolManager.Instance.InstantiateObjects("xrangeenemy");
        ObjectPoolManager.Instance.InstantiateObjects("meleeenemy");
        ObjectPoolManager.Instance.InstantiateObjects("DropItem");
        ObjectPoolManager.Instance.InstantiateObjects("Coin");
        ObjectPoolManager.Instance.InstantiateObjects("slug");
        ObjectPoolManager.Instance.InstantiateObjects("Golem");
        StartLevel = false;
        enemies = new List<GameObject>();
        RoomManager.Instance.RoomChanged += RoomChange;
        //time = firstDelayTime;
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
        CheckRoomSpawn();
    }

    void Update()
    {
        //if (StartLevel)
        //{
        // check all spawns in current room
        if (currentRoom.roomSpawns.Count != 0)
        {
            if (!currentRoom.isBossRoom)
            {
                for (int i = 0; i < currentRoom.roomSpawns.Count; ++i)
                {
                    //if (currentRoom.roomSpawns[i].CurrentWave == currentRoom.roomSpawns[i].Wave.waveData.Count)
                    //{
                    //    currentRoom.roomSpawns[i].gameObject.SetActive(false);
                    //}

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
                        //else
                        //{
                        //    Debug.LogError("Drop number can not larger than enemy total number Room: " + i.ToString() + " Total Spawn Number: " + currentRoom.roomSpawns[i].TotalEnemies.ToString());
                        //}
                        d++;
                    }

                    for (int e = 0; e < currentRoom.roomSpawns[i].Enemies.Count; ++e)
                    {
                        if (currentRoom.roomSpawns[i].Enemies[e].activeInHierarchy == false)
                        {
                            //check which enemy will drop
                            //for (int d = 0; d < currentRoom.roomSpawns[i].DropNumber.Count; ++d)
                            //{
                            //    if (e == currentRoom.roomSpawns[i].DropNumber[d])
                            //    {
                            //        enemyObject = ObjectPoolManager.Instance.GetPooledObject("DropItem");
                            //        enemyObject.transform.position = currentRoom.roomSpawns[i].Enemies[e].transform.position;
                            //        enemyObject.SetActive(true);
                            //        coin = ObjectPoolManager.Instance.GetPooledObject("Coin");
                            //        coin.transform.position = currentRoom.roomSpawns[i].Enemies[e].transform.position + new Vector3(1, 0, 0);
                            //        coin.SetActive(true);
                            //        currentRoom.roomSpawns[i].DropNumber.RemoveAt(d);
                            //        currentRoom.roomSpawns[i].ItemDropCount++;
                            //        break;
                            //    }
                            //}

                            //remove dead enemies
                            //if (currentRoom.roomSpawns[i].ItemDropCount == currentRoom.roomSpawns[i].ItemDropNum)
                            //{
                            if (currentRoom.roomSpawns[i].DropNumber.Count != currentRoom.roomSpawns[i].ItemDropCount)
                            {
                                enemyObject = ObjectPoolManager.Instance.GetPooledObject("DropItem");
                                enemyObject.transform.position = currentRoom.roomSpawns[i].Enemies[e].transform.position;
                                enemyObject.SetActive(true);
                                currentRoom.roomSpawns[i].ItemDropCount++;
                            }
                            coin = ObjectPoolManager.Instance.GetPooledObject("Coin");
                            coin.transform.position = currentRoom.roomSpawns[i].Enemies[e].transform.position + new Vector3(1, 0, 0);
                            coin.SetActive(true);
                            currentRoom.roomSpawns[i].Enemies.RemoveAt(e);
                            currentRoom.roomSpawns[i].TotalEnemies--;
                            currentRoom.roomSpawns[i].TotalWaveSpawn--;
                            Debug.Log(currentRoom.roomSpawns[i].TotalEnemies);
                            // }


                            //check wave clear
                            if (currentRoom.roomSpawns[i].TotalWaveSpawn == 0)
                            {
                                currentRoom.roomSpawns[i].IsWaveClear = true;
                            }


                        }
                    }
                    if (currentRoom.roomSpawns[i].WaveDelayTurnOn)
                    {
                        if (firstSpawn)
                        {
                            if (currentRoom.roomSpawns[i].DelayTime <= 0.0f)
                            {
                                SpawnWave(i);
                                currentRoom.roomSpawns[i].DelayTime = currentRoom.roomSpawns[i].WaveDelay;
                            }
                            currentRoom.roomSpawns[i].DelayTime -= Time.deltaTime;
                        }
                        else
                        {
                            if (currentRoom.roomSpawns[i].DelayTime <= 0.0f)
                            {
                                if (time <= 0.0f)
                                {
                                    SpawnWave(i);
                                    time = firstDelayTime;
                                    if (i == currentRoom.roomSpawns.Count)
                                    {
                                        firstSpawn = false;
                                    }
                                }
                                time -= Time.deltaTime;
                                currentRoom.roomSpawns[i].DelayTime = currentRoom.roomSpawns[i].WaveDelay;
                            }
                            currentRoom.roomSpawns[i].DelayTime -= Time.deltaTime;
                        }

                    }
                    else
                    {

                        if (currentRoom.roomSpawns[i].IsFirstSpawn)
                        {
                            if (time <= 0.0f)
                            {
                                SpawnWave(i);
                                time = firstDelayTime;
                            }
                            time -= Time.deltaTime;
                        }
                        else
                        {
                            SpawnWave(i);
                        }

                    }
                }
                CheckRoomClear();
            }
            else
            {
                BossSpawn();
            }
        }
    }

    void Reuse()
    {
        currentRoom.bossSkillOver = false;
        foreach (var spawn in currentRoom.roomSpawns)
        {
            spawn.gameObject.SetActive(true);
        }
        for (int i = 0; i < currentRoom.roomSpawns.Count; ++i)
        {

            currentRoom.roomSpawns[i].CurrentWave = 0;
            currentRoom.roomSpawns[i].SpawnClear = false;
            currentRoom.roomSpawns[i].gameObject.SetActive(true);
            for (int s = 0; s < currentRoom.roomSpawns[i].Wave.waveData.Count; ++s)
            {
                currentRoom.roomSpawns[i].TotalEnemies += currentRoom.roomSpawns[i].Wave.waveData[s].spawnNumber;
                currentRoom.roomSpawns[i].TotalWaveSpawn = currentRoom.roomSpawns[i].Wave.waveData[0].spawnNumber;
            }
        }
    }

    public void BossSpawn()
    {
        Reuse();
        int spawnCount =0;
        for (int i = 0; i < currentRoom.roomSpawns.Count; ++i)
        {
            if (currentRoom.roomSpawns[i].TotalWaveSpawn == 0)
            {
                currentRoom.roomSpawns[i].IsWaveClear = true;
            }

            if (currentRoom.roomSpawns[i].IsFirstSpawn)
            {
                if (time <= 0.0f)
                {
                    SpawnWave(i);
                    time = firstDelayTime;
                }
                time -= Time.deltaTime;
            }
            else
            {
                SpawnWave(i);
            }

            if (currentRoom.roomSpawns[i].CurrentWave == currentRoom.roomSpawns[i].Wave.waveData.Count && currentRoom.roomSpawns[i].TotalEnemies == 0 && !currentRoom.roomSpawns[i].SpawnClear)
            {
                currentRoom.roomSpawns[i].SpawnClear = true;
                currentRoom.roomSpawns[i].gameObject.SetActive(false);
                spawnCount++;
            }
            else if (checkcount == currentRoom.roomSpawns.Count -1)
            {
                currentRoom.bossSkillOver= true;
            }
        }
    }

    private void RoomChange(int changeId)
    {
        time = 0.0f;
        roomNumber = changeId;
        roomClear = false;
        if (roomNumber != lastroom)
        {
            lastroom = roomNumber;
            checkcount = 0;
        }
        CheckRoomSpawn();
    }

    void CheckRoomSpawn()
    {
        currentRoom = rooms[roomNumber];
        if (!currentRoom.isBossRoom)
        {
            foreach (var spawn in currentRoom.roomSpawns)
            {
                spawn.gameObject.SetActive(true);
            }

            //count the wave total enemies
            for (int i = 0; i < currentRoom.roomSpawns.Count; ++i)
            {
                for (int s = 0; s < currentRoom.roomSpawns[i].Wave.waveData.Count; ++s)
                {
                    currentRoom.roomSpawns[i].TotalEnemies += currentRoom.roomSpawns[i].Wave.waveData[s].spawnNumber;
                    currentRoom.roomSpawns[i].TotalWaveSpawn = currentRoom.roomSpawns[i].Wave.waveData[0].spawnNumber;
                }
            }
            isRoomCheck = true;
        }
    }

    void CheckRoomClear()
    {
        if (currentRoom.roomSpawns.Count != 0)
        {
            for (int i = 0; i < currentRoom.roomSpawns.Count; ++i)
            {
                if (currentRoom.roomSpawns[i].CurrentWave == currentRoom.roomSpawns[i].Wave.waveData.Count && currentRoom.roomSpawns[i].TotalEnemies == 0 && !currentRoom.roomSpawns[i].SpawnClear)
                {
                    currentRoom.roomSpawns[i].SpawnClear = true;
                    currentRoom.roomSpawns[i].gameObject.SetActive(false);
                    checkcount++;
                }

                if (checkcount == currentRoom.roomSpawns.Count)
                {
                    if (currentRoom.roomSpawns[i].Enemies.Count == 0)
                    {
                        isRoomCheck = false;
                        RoomClear = true;
                        currentRoom.clear = true;
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

    void SpawnType(int count)
    {
        if (currentRoom.roomSpawns[count].SpawnCount < currentRoom.roomSpawns[count].Wave.waveData[currentRoom.roomSpawns[count].CurrentWave].spawnNumber)
        {
            for (int c = 0; c < currentRoom.roomSpawns[count].Wave.waveData[currentRoom.roomSpawns[count].CurrentWave].spawnNumber; c++)
            {
                //if (currentRoom.roomSpawns[count].WaveDelayTurnOn)
                //{
                //    currentRoom.roomSpawns[count].TotalEnemies--;
                //}

                //spawn enemy
                if (currentRoom.roomSpawns[count].SpawnDelayTime <= 0.0f)
                {
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
                        case Wave.Enemytype.armorenemy:
                            spawnMax = new Vector2(currentRoom.roomSpawns[count].SpMax.x, currentRoom.roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(currentRoom.roomSpawns[count].SpMin.x, currentRoom.roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)currentRoom.roomSpawns[count].SpMin.x, (int)currentRoom.roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)currentRoom.roomSpawns[count].SpMin.y, (int)currentRoom.roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("armorenemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            currentRoom.roomSpawns[count].Enemies.Add(enemyObject);
                            currentRoom.roomSpawns[count].SpawnCount++;
                            print("spawn");
                            break;
                        case Wave.Enemytype.bombenemy:
                            spawnMax = new Vector2(currentRoom.roomSpawns[count].SpMax.x, currentRoom.roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(currentRoom.roomSpawns[count].SpMin.x, currentRoom.roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)currentRoom.roomSpawns[count].SpMin.x, (int)currentRoom.roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)currentRoom.roomSpawns[count].SpMin.y, (int)currentRoom.roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("bombenemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            currentRoom.roomSpawns[count].Enemies.Add(enemyObject);
                            currentRoom.roomSpawns[count].SpawnCount++;
                            print("spawn");
                            break;
                        case Wave.Enemytype.boomerangeenemy:
                            spawnMax = new Vector2(currentRoom.roomSpawns[count].SpMax.x, currentRoom.roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(currentRoom.roomSpawns[count].SpMin.x, currentRoom.roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)currentRoom.roomSpawns[count].SpMin.x, (int)currentRoom.roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)currentRoom.roomSpawns[count].SpMin.y, (int)currentRoom.roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("boomerangeenemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            currentRoom.roomSpawns[count].Enemies.Add(enemyObject);
                            currentRoom.roomSpawns[count].SpawnCount++;
                            print("spawn");
                            break;
                        case Wave.Enemytype.meleeenemy:
                            spawnMax = new Vector2(currentRoom.roomSpawns[count].SpMax.x, currentRoom.roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(currentRoom.roomSpawns[count].SpMin.x, currentRoom.roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)currentRoom.roomSpawns[count].SpMin.x, (int)currentRoom.roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)currentRoom.roomSpawns[count].SpMin.y, (int)currentRoom.roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("meleeenemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            currentRoom.roomSpawns[count].Enemies.Add(enemyObject);
                            currentRoom.roomSpawns[count].SpawnCount++;
                            print("spawn");
                            break;
                        case Wave.Enemytype.xrangeenemy:
                            spawnMax = new Vector2(currentRoom.roomSpawns[count].SpMax.x, currentRoom.roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(currentRoom.roomSpawns[count].SpMin.x, currentRoom.roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)currentRoom.roomSpawns[count].SpMin.x, (int)currentRoom.roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)currentRoom.roomSpawns[count].SpMin.y, (int)currentRoom.roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("xrangeenemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            currentRoom.roomSpawns[count].Enemies.Add(enemyObject);
                            currentRoom.roomSpawns[count].SpawnCount++;
                            print("spawn");
                            break;
                        case Wave.Enemytype.slug:
                            spawnMax = new Vector2(currentRoom.roomSpawns[count].SpMax.x, currentRoom.roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(currentRoom.roomSpawns[count].SpMin.x, currentRoom.roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)currentRoom.roomSpawns[count].SpMin.x, (int)currentRoom.roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)currentRoom.roomSpawns[count].SpMin.y, (int)currentRoom.roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("slug");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            currentRoom.roomSpawns[count].Enemies.Add(enemyObject);
                            currentRoom.roomSpawns[count].SpawnCount++;
                            print("spawn");
                            break;
                        case Wave.Enemytype.golem:
                            spawnMax = new Vector2(currentRoom.roomSpawns[count].SpMax.x, currentRoom.roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(currentRoom.roomSpawns[count].SpMin.x, currentRoom.roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)currentRoom.roomSpawns[count].SpMin.x, (int)currentRoom.roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)currentRoom.roomSpawns[count].SpMin.y, (int)currentRoom.roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("Golem");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            currentRoom.roomSpawns[count].Enemies.Add(enemyObject);
                            currentRoom.roomSpawns[count].SpawnCount++;
                            print("spawn");
                            break;

                        default:
                            break;
                    }
                    currentRoom.roomSpawns[count].IsFirstSpawn = false;
                    currentRoom.roomSpawns[count].SpawnDelayTime = currentRoom.roomSpawns[count].SpawnDelay;
                }
                currentRoom.roomSpawns[count].SpawnDelayTime -= Time.deltaTime;
            }
        }

        //check the wave spawn all enemies or not
        if (currentRoom.roomSpawns[count].IsWaveClear)
        {
            // currentRoom.roomSpawns[count].CurrentWave++;
            currentRoom.roomSpawns[count].TotalWaveSpawn = currentRoom.roomSpawns[count].Wave.waveData[currentRoom.roomSpawns[count].CurrentWave].spawnNumber;
            currentRoom.roomSpawns[count].CurrentWave++;
            currentRoom.roomSpawns[count].SpawnCount = 0;
            currentRoom.roomSpawns[count].IsWaveClear = false;

        }

    }

    void SpawnWave(int count)
    {
        if (currentRoom.roomSpawns[count].WaveDelayTurnOn)
        {
            if (currentRoom.roomSpawns[count].CurrentWave < currentRoom.roomSpawns[count].Wave.waveData.Count)
            {
                SpawnType(count);
            }
        }
        else
        {
            //check this wave 
            if (currentRoom.roomSpawns[count].CurrentWave < currentRoom.roomSpawns[count].Wave.waveData.Count)
            {
                SpawnType(count);
            }
        }
    }
}
