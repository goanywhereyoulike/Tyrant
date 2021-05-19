using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    EnemyState enemyState = new EnemyState();

    public Transform target;
   
    private float distance = 0f;
    private float attatckdistance = 5f;

    StaticMachine behaviours = new StaticMachine();
   
    public EnemyState EnemyState { get => enemyState; set => enemyState = value; }


    // Start is called before the first frame update
    void Start()
    {
        behaviours = gameObject.GetComponent<StaticMachine>();
        behaviours.setEnemy(this);
        behaviours.AllBehaviour();
    }

    // Update is called once per frame
    void Update()
    {
        behaviours.Update();
        distance = Vector3.Distance(transform.position, target.position);
        if (distance < attatckdistance)
        {
            enemyState.force = behaviours.ForceCalculate();
            enemyState.acceleration = enemyState.force / enemyState.Mass;
            enemyState.velocity += enemyState.acceleration;
        }
        else
        {
            enemyState. velocity = Vector3.zero;
        }
         transform.position += enemyState.velocity;
    }
}