using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    Enemy nEnemy;
    public GameObject Enemy;

   // public Transform[] spawnPoints;
    void Start()
    {
      
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
       
        Instantiate(Enemy, new Vector2(0f, 0f), Quaternion.identity);
        //Instantiate(Enemy, new Vector2(0f, 0f), Quaternion.identity);
    }
}