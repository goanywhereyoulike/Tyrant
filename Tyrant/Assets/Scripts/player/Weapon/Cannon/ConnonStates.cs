using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Connon", menuName = "Player/Weapon/Connon", order = 1)]
public class ConnonStates : WeaponStates
{
    [SerializeField] private float shootingRange;
    public float ShootingRange { get => shootingRange; set => shootingRange = value; }
}

