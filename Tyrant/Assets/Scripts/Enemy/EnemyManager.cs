using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager Instance { get => _instance; }

  //  List<Enemy> enemies = new List<Enemy>();
    public int wavenumber=5;
    public int noSpawn;
    public Tilemap tilemap;
    List<GameObject> enemies = new List<GameObject>();
    Enemy enemy;
    Vector2 spawnPosition;
    Vector2 insideMax;
    Vector2 insideMin;
    int spawnX;
    int spawnY;
    int spawnCount;
    GameObject enemyObject;
    // public Transform[] spawnPoints;
    private void Start()
    {
        ObjectPoolManager.Instance.InstantiateObjects("normalenemy");
        if (_instance == null)
        {
            _instance = this;
        }
        else if(_instance !=this)
        {
            Destroy(this.enemyObject);
        }
    }

    void Update()
    {
        insideMax = new Vector2(tilemap.cellBounds.xMax, tilemap.cellBounds.yMax) / noSpawn;
        insideMin = new Vector2(tilemap.cellBounds.xMin, tilemap.cellBounds.yMin) / noSpawn;


        if(noSpawn<2)
        {
            Debug.Log("can't spawn");
            return;
        }

        for(int i =0;i< enemies.Count;++i)
        {
            if(enemies[i].activeInHierarchy ==false)
            {
                enemies.RemoveAt(i);
                spawnCount--;
            }
        }

        if (spawnCount == 0)
        {
            while (spawnCount != wavenumber)
            {
                spawnX = Random.Range(tilemap.cellBounds.xMin, tilemap.cellBounds.xMax);
                spawnY = Random.Range(tilemap.cellBounds.yMin, tilemap.cellBounds.yMax);
                spawnPosition = new Vector2(spawnX, spawnY);
                if (CheckPosition(spawnPosition))
                {
                    EnemySpawn(spawnPosition);
                    print("spawn");
                }
            }
        }
    }
    void EnemySpawn(Vector2 position)
    {
        enemyObject = ObjectPoolManager.Instance.GetPooledObject("normalenemy");
        enemyObject.SetActive(true);
        enemyObject.transform.position = position;
        enemies.Add(enemyObject);
        spawnCount++;
    }

    bool CheckPosition(Vector2 position)
    {
        for (int i = (int)insideMin.x; i <= (int)insideMax.x; ++i)
        {
            for (int j = (int)insideMin.y; j <= (int)insideMax.y; ++j)
            {
                if (position == new Vector2(i, j))
                {
                    return false;
                }
            }
        }
        return true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(insideMin, insideMax);
    }
}