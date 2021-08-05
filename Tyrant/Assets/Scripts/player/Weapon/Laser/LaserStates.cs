using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Laser", menuName = "Player/Weapon/Laser", order = 3)]
public class LaserStates : WeaponStates
{
    [SerializeField]
    private float chargingTime = 0.0f;
    [SerializeField]
    private float shootingRange = 0.0f;
    [SerializeField]
    private float holdingTime = 0.0f;

    public float ChargingTime { get => chargingTime; set => chargingTime = value; }
    public float ShootingRange { get => shootingRange; set => shootingRange = value; }
    public float HoldingTime { get => holdingTime; set => holdingTime = value; }
}
