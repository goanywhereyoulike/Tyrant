using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager Instance { get => _instance; }

  //  List<Enemy> enemies = new List<Enemy>();
    public int wavenumber=5;
    public GameObject[] enemies;
    Enemy enemy;
  
    GameObject gameObject;
    // public Transform[] spawnPoints;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        ObjectPoolManager.Instance.InstantiateObjects("normalenemy");
        if (_instance == null)
        {
            _instance = this;
        }
        else if(_instance !=this)
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Space))
        {
            EnemySpawn();
            print("spawn");
        }
    }
    void EnemySpawn()
    {
        gameObject = ObjectPoolManager.Instance.GetPooledObject("normalenemy");
        gameObject.SetActive(true);
        gameObject.transform.position = new Vector3(0, 0, 0);
        //Instantiate(enemies[0], new Vector2(0f, 0f), Quaternion.identity);
        //Instantiate(Enemy, new Vector2(0f, 0f), Quaternion.identity);
    }
}