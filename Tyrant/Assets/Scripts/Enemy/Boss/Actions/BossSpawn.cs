using UnityEngine;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;

public class BossSpawn : Action
{
    List<GameObject> enemies;


    List<SpawnArea> roomSpawns;
    
    //public List<room> rooms = new List<room>();

    Vector2 spawnPosition;
    Vector2 spawnMax;
    Vector2 spawnMin;

    Collider2D m_Collider;

    int spawnX;
    int spawnY;
    int spawnCount;

    [SerializeField]
    private int roomNumber;

    [SerializeField]
    private int Delay=5;

    private bool roomClear;
    private bool isSpawn;
    private bool isRoomCheck;
    private bool canSpawn;

    private int checkcount;
    private int enemyDrop;

    private float DelayTime = 0.0f;

    private PSC psc;

    private GameObject enemyObject;

    private SpriteRenderer spr;

    public bool IsSpawn { get => isSpawn; set => isSpawn = value; }

    // Start is called before the first frame update
    public override void OnStart()
    {
        ObjectPoolManager.Instance.InstantiateObjects("normalenemy");
        ObjectPoolManager.Instance.InstantiateObjects("rangeEnemy");
        ObjectPoolManager.Instance.InstantiateObjects("Level1Boss");
        ObjectPoolManager.Instance.InstantiateObjects("bombenemy");
        ObjectPoolManager.Instance.InstantiateObjects("armorenemy");
        ObjectPoolManager.Instance.InstantiateObjects("DropItem");
        roomSpawns = new List<SpawnArea>();
        m_Collider = GetComponent<Collider2D>();
        psc = GetComponent<PSC>();
        spr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public override TaskStatus OnUpdate()
    {
        if (!IsSpawn)
        {
            if (!roomClear)
            {
                m_Collider.enabled = false;
                spr.material.color = Color.yellow;
            }
            if (!isRoomCheck)
                CheckRoomSpawn();

            CheckRoomClear();

            if (roomSpawns.Count != 0)
            {
                for (int i = 0; i < roomSpawns.Count; ++i)
                {
                    if (roomSpawns[i].CurrentWave == roomSpawns[i].Wave.waveData.Count)
                    {
                        //roomSpawns[i].gameObject.SetActive(false);
                    }

                    if (roomSpawns[i].WaveDelayTurnOn)
                    {
                        if (roomSpawns[i].DelayTime <= 0.0f)
                        {
                            SpawnWave(i);

                            //roomSpawns[i].CurrentWave++;
                            roomSpawns[i].DelayTime = roomSpawns[i].WaveDelay;
                        }
                        roomSpawns[i].DelayTime -= Time.deltaTime;
                    }
                    else
                    {
                        if (DelayTime <= 0.0f)
                        {
                            canSpawn = true;
                            DelayTime = Delay;
                        }
                        DelayTime -= Time.deltaTime;
                        if (canSpawn)
                        {
                            SpawnWave(i);
                            canSpawn = false;
                        }
                    }

                    //check how many enemy can drop item
                    for (int d = roomSpawns[i].ChooseEnemyDrop; d < roomSpawns[i].ItemDropNum;)
                    {
                        enemyDrop = Random.Range(0, roomSpawns[i].TotalEnemies);
                        if (roomSpawns[i].ItemDropNum <= roomSpawns[i].TotalEnemies)
                        {
                            while (roomSpawns[i].DropNumber.Contains(enemyDrop))
                            {
                                enemyDrop = Random.Range(0, roomSpawns[i].TotalEnemies);
                            }
                            roomSpawns[i].DropNumber.Add(enemyDrop);
                            roomSpawns[i].ChooseEnemyDrop++;
                        }
                        else
                        {
                            Debug.LogError("Drop number can not larger than enemy total number Room: " + i.ToString() + " Total Spawn Number: " + roomSpawns[i].TotalEnemies.ToString());
                        }
                        d++;
                    }

                    for (int e = 0; e < roomSpawns[i].Enemies.Count; ++e)
                    {
                        if (roomSpawns[i].Enemies[e].activeInHierarchy == false)
                        {
                            //check which enemy will drop
                            for (int d = 0; d < roomSpawns[i].DropNumber.Count; ++d)
                            {
                                if (e == roomSpawns[i].DropNumber[d])
                                {
                                    enemyObject = ObjectPoolManager.Instance.GetPooledObject("DropItem");
                                    enemyObject.transform.position = roomSpawns[i].Enemies[e].transform.position;
                                    enemyObject.SetActive(true);
                                    roomSpawns[i].DropNumber.RemoveAt(d);
                                    roomSpawns[i].ItemDropCount++;
                                    break;
                                }
                            }

                            if (roomSpawns[i].ItemDropCount == roomSpawns[i].ItemDropNum)
                            {
                               roomSpawns[i].Enemies.RemoveAt(e);
                            }

                            roomSpawns[i].SpawnCount--;
                        }
                    }
                }
            }

            if (roomClear)
           {
                m_Collider.enabled = true;
                IsSpawn = true;
                spr.material.color = Color.white;
                return TaskStatus.Success;
            }
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }

    void CheckRoomClear()
    {
        if (roomSpawns.Count != 0)
        {
            for (int i = 0; i < roomSpawns.Count; ++i)
            {
                if (roomSpawns[i].CurrentWave == roomSpawns[i].Wave.waveData.Count && roomSpawns[i].SpawnCount == 0 && !roomSpawns[i].SpawnClear)
                {
                    roomSpawns[i].SpawnClear = true;
                    checkcount++;
                }

                if (checkcount == roomSpawns.Count)
                {
                    roomClear = true;
                    break;
                }
            }
        }
    }

    void CheckRoomSpawn()
    {
        var Spawns = GameObjectsLocator.Instance.Get<SpawnArea>();
        for(int s=0;s<Spawns.Count;s++)
        {
            if (Spawns[s].roomNumber == roomNumber && Spawns[s].name != "BossSpawnArea")
                roomSpawns.Add(Spawns[s]);
        }
      
        for (int i = 0; i < roomSpawns.Count; ++i)
        {
            for (int s = 0; s < roomSpawns[i].Wave.waveData.Count; ++s)
            {
                roomSpawns[i].TotalEnemies += roomSpawns[i].Wave.waveData[s].spawnNumber;
            }
        }
        isRoomCheck = true;
    }

    void SpawnWave(int count)
    {
        int c;
        if (roomSpawns[count].WaveDelayTurnOn)
        {
            if (roomSpawns[count].CurrentWave < roomSpawns[count].Wave.waveData.Count)
            {
                for (c = 0; c < roomSpawns[count].Wave.waveData[roomSpawns[count].CurrentWave].spawnNumber; ++c)
                {
                    switch (roomSpawns[count].Wave.waveData[roomSpawns[count].CurrentWave].enemytype)
                    {
                        case Wave.Enemytype.normalenemy:
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
                            break;
                        case Wave.Enemytype.rangeEnemy:
                            spawnMax = new Vector2(roomSpawns[count].SpMax.x, roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(roomSpawns[count].SpMin.x, roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)roomSpawns[count].SpMin.x, (int)roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)roomSpawns[count].SpMin.y, (int)roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("rangeEnemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            roomSpawns[count].Enemies.Add(enemyObject);
                            roomSpawns[count].SpawnCount++;
                            break;
                        case Wave.Enemytype.Level1Boss:
                            spawnMax = new Vector2(roomSpawns[count].SpMax.x, roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(roomSpawns[count].SpMin.x, roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)roomSpawns[count].SpMin.x, (int)roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)roomSpawns[count].SpMin.y, (int)roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("Level1Boss");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            roomSpawns[count].Enemies.Add(enemyObject);
                            roomSpawns[count].SpawnCount++;
                            break;
                        case Wave.Enemytype.armorenemy:
                            spawnMax = new Vector2(roomSpawns[count].SpMax.x, roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(roomSpawns[count].SpMin.x, roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)roomSpawns[count].SpMin.x, (int)roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)roomSpawns[count].SpMin.y, (int)roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("armorenemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            roomSpawns[count].Enemies.Add(enemyObject);
                            roomSpawns[count].SpawnCount++;
                            break;
                        case Wave.Enemytype.bombenemy:
                            spawnMax = new Vector2(roomSpawns[count].SpMax.x, roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(roomSpawns[count].SpMin.x, roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)roomSpawns[count].SpMin.x, (int)roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)roomSpawns[count].SpMin.y, (int)roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("bombenemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            roomSpawns[count].Enemies.Add(enemyObject);
                            roomSpawns[count].SpawnCount++;
                            break;
                        default:
                            break;
                    }
                }
                //if (c == roomSpawns[count].Wave.waveData[c].spawnNumber)
                //{
                //    c = 0;
                //}
                roomSpawns[count].CurrentWave++;
            }
        }
        else
        {
            if (roomSpawns[count].SpawnCount == 0 && roomSpawns[count].CurrentWave < roomSpawns[count].Wave.waveData.Count)
            {
                for (c = 0; c < roomSpawns[count].Wave.waveData[roomSpawns[count].CurrentWave].spawnNumber; ++c)
                {
                    //spawn position
                    switch (roomSpawns[count].Wave.waveData[roomSpawns[count].CurrentWave].enemytype)
                    {
                        case Wave.Enemytype.normalenemy:
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
                            break;
                        case Wave.Enemytype.rangeEnemy:
                            spawnMax = new Vector2(roomSpawns[count].SpMax.x, roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(roomSpawns[count].SpMin.x, roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)roomSpawns[count].SpMin.x, (int)roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)roomSpawns[count].SpMin.y, (int)roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("rangeEnemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            roomSpawns[count].Enemies.Add(enemyObject);
                            roomSpawns[count].SpawnCount++;
                            break;
                        case Wave.Enemytype.Level1Boss:
                            spawnMax = new Vector2(roomSpawns[count].SpMax.x, roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(roomSpawns[count].SpMin.x, roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)roomSpawns[count].SpMin.x, (int)roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)roomSpawns[count].SpMin.y, (int)roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("Level1Boss");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            roomSpawns[count].Enemies.Add(enemyObject);
                            roomSpawns[count].SpawnCount++;
                            break;
                        case Wave.Enemytype.armorenemy:
                            spawnMax = new Vector2(roomSpawns[count].SpMax.x, roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(roomSpawns[count].SpMin.x, roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)roomSpawns[count].SpMin.x, (int)roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)roomSpawns[count].SpMin.y, (int)roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("armorenemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            roomSpawns[count].Enemies.Add(enemyObject);
                            roomSpawns[count].SpawnCount++;
                            break;
                        case Wave.Enemytype.bombenemy:
                            spawnMax = new Vector2(roomSpawns[count].SpMax.x, roomSpawns[count].SpMax.y);
                            spawnMin = new Vector2(roomSpawns[count].SpMin.x, roomSpawns[count].SpMin.y);
                            spawnX = Random.Range((int)roomSpawns[count].SpMin.x, (int)roomSpawns[count].SpMax.x);
                            spawnY = Random.Range((int)roomSpawns[count].SpMin.y, (int)roomSpawns[count].SpMax.y);
                            spawnPosition = new Vector2(spawnX, spawnY);

                            enemyObject = ObjectPoolManager.Instance.GetPooledObject("bombenemy");
                            enemyObject.transform.position = spawnPosition;
                            enemyObject.SetActive(true);
                            roomSpawns[count].Enemies.Add(enemyObject);
                            roomSpawns[count].SpawnCount++;
                            break;
                        default:
                            break;
                    }
                }
                roomSpawns[count].CurrentWave++;
            }
        }
    }
}
