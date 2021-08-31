using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSC : MonoBehaviour
{
    [SerializeField]
    private EnemyState enemyState;

    public EnemyState EnemyState { get => enemyState; set => enemyState = value; }

    private float health = 100.0f;
    public bool IsDied
    {
        get
        {
            return health <= 0f;
        }
    }
}
