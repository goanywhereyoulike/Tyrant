using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyState
{
    [SerializeField]
    private float enemyHealth;    // default value
    [SerializeField]
    private float enemyDamage;    // default value
    [SerializeField]
    private float enemyMoveSpeed;      // default value
    [SerializeField]
    private float mass = 1f;
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private float stopDistance;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float detectRange;

    public Vector3 velocity { get; set; }
    public Vector3 acceleration { get; set; }
    public Vector3 force { get; set; }

    public float EnemyHealth { get => enemyHealth; }
    public float EnemyDamage { get => enemyDamage; }
    public float EnemyMoveSpeed { get => enemyMoveSpeed; }
    public float Mass { get => mass; }
    public float TimeBetweenAttacks { get => timeBetweenAttacks; set =>timeBetweenAttacks= value; }
    public float StopDistance { get => stopDistance; }
    public float AttackSpeed { get => attackSpeed; }

    public float DetectRange { get => detectRange; }
}
