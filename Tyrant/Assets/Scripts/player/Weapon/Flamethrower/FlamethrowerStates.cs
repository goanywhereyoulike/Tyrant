using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Flamethrower", menuName = "Player/Weapon/Flamethrower", order = 2)]
public class FlamethrowerStates : WeaponStates
{
    [SerializeField] private float bulletExistTime;
    [SerializeField] private float burnDamage;
    [SerializeField] private float maxAmmo;
    public float BulletExistTime { get => bulletExistTime; set => bulletExistTime = value; }
    public float BurnDamage { get => burnDamage; set => burnDamage = value; }
    public float MaxAmmo { get => maxAmmo; set => maxAmmo = value; }
}