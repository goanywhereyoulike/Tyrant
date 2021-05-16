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


    BehavioursChange behaviours = new BehavioursChange();
    Seeking seek = new Seeking();
    Arrive arrive = new Arrive();
    public EnemyState EnemyState { get => enemyState; set => enemyState = value; }


    // Start is called before the first frame update
    void Start()
    {
        behaviours.setEnemy(this);
        behaviours.AddBehaviour(seek);
        seek.Pause();
        behaviours.AddBehaviour(arrive);   
    }

    // Update is called once per frame
    void Update()
    {
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