using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Flamethrower", menuName = "Player/Weapon/Flamethrower", order = 2)]
public class FlamethrowerStates : WeaponStates
{
    [SerializeField] private float bulletExistTime;
    public float BulletExistTime { get => bulletExistTime; set => bulletExistTime = value; }
}