using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyState
{
    [SerializeField]
    private float maxHealth;    // default value
    [SerializeField]
    private float maxDamage;    // default value
    [SerializeField]
    private float maxMoveSpeed;      // default value
    [SerializeField]
    private float mass;
    [SerializeField]
    private float timeBetweenAttacks;
    [SerializeField]
    private float stopDistance;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float detectRange;
    [SerializeField]
    private float maxArmor;

    public Vector3 velocity { get; set; }
    public Vector3 acceleration { get; set; }
    public Vector3 force { get; set; }

    public float MaxHealth { get => maxHealth; }
    public float MaxDamage { get => maxDamage; }
    public float MaxMoveSpeed { get => maxMoveSpeed; }
    public float Mass { get => mass; }
    public float TimeBetweenAttacks { get => timeBetweenAttacks;}
    public float StopDistance { get => stopDistance; }
    public float AttackSpeed { get => attackSpeed; }

    public float DetectRange { get => detectRange; }
    public float MaxArmor { get => maxArmor; set => maxArmor = value; }
}
