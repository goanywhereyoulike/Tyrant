using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyState 
{
    [SerializeField]
    private float enemyHealth = 0f;    // default value
    [SerializeField]
    private float enemyDamage = 0f;    // default value
    [SerializeField]
    private float enemyMoveSpeed = 0f;      // default value
    [SerializeField]
    private float mass = 1f;
    [SerializeField]
    private float timeBetweenAttacks =0f;
    [SerializeField]
    private float stopDistance = 0f;
    [SerializeField]
    private float attackSpeed = 0f;


    public Vector3 velocity { get; set; }
    public Vector3 acceleration { get; set; }
    public Vector3 force { get; set; }

    public float EnemyHealth { get => enemyHealth; }
    public float EnemyDamage { get => enemyDamage; }
    public float EnemyMoveSpeed { get => enemyMoveSpeed; }
    public float Mass { get => mass; }
    public float TimeBetweenAttacks { get => timeBetweenAttacks; }
    public float StopDistance { get => stopDistance; }
    public float AttackSpeed { get => attackSpeed; }

}
