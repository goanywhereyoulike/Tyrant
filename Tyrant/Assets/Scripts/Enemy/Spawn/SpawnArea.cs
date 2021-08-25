using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnArea : MonoBehaviour, GameObjectsLocator.IGameObjectRegister
{
    [SerializeField]
    private float waveDelay;
    [SerializeField]
    private string enemyType;
    [SerializeField]

    private Wave wave;
    private int spawnCount;
    private int currentWave;
    private float delayTime;
    private bool spawnClear;

    public int spWidth;
    public int spHeight;
    public int roomNumber;

    List<GameObject> enemies;

    Collider2D mCollider;
    private Vector2 spMax;
    private Vector2 spMin;
    public Vector2 SpMax { get => spMax; }
    public Vector2 SpMin { get => spMin; }
    public string EnemyType { get => enemyType; }
    public float WaveDelay { get => waveDelay; }
    public int CurrentWave { get => currentWave; set => currentWave = value; }
    public int SpawnCount { get => spawnCount; set => spawnCount = value; }
    public List<GameObject> Enemies { get => enemies; set => enemies = value; }
    public float DelayTime { get => delayTime; set => delayTime = value; }
    public bool SpawnClear { get => spawnClear; set => spawnClear = value; }
    public Wave Wave { get => wave;}


    //  public string[] enemyType = { "normalenemy", "rangeEnemy" };
    //
    // public List<string> waveSpawn;

    private void Awake()
    {
        //waveSpawn = new List<string>(waveNumber);
        mCollider = GetComponent<Collider2D>();
        Area();
        Enemies = new List<GameObject>();
        
        RegisterToLocator();
    }

    void Area()
    {
        spMax = mCollider.bounds.max;
        spMin = mCollider.bounds.min;
    }

    public void RegisterToLocator()
    {
        GameObjectsLocator.Instance.Register<SpawnArea>(this);
    }

    public void UnRegisterToLocator()
    {
        GameObjectsLocator.Instance.Unregister<SpawnArea>(this);
        gameObject.SetActive(false);
    }
}

