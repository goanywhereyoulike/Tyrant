using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerState", menuName = "Player/State", order = 1)]
public class PlayerStates : ScriptableObject
{
    [SerializeField] private float maxHealth = 0.0f;
    [SerializeField] private float moveSpeed = 0.01f;

    public Action MoveSpeedChanged = null;
    public Action MaxHealthChanged = null;

    public float MaxHealth
    {
        get => maxHealth;
        set
        {
            maxHealth = value;
            MaxHealthChanged?.Invoke();
        }
    }

    public float MoveSpeed
    {
        get => moveSpeed;
        set
        {
            moveSpeed = value;
            MoveSpeedChanged?.Invoke();
        }
    }
}
