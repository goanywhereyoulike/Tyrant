using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponStates : ScriptableObject
{
    [SerializeField] protected float damage;
    [SerializeField] protected float reloadTime;
    [SerializeField] protected float shootingDelayTime;
    [SerializeField] protected float bulletShootingSpeed;
    [SerializeField] protected int maxBulletCount;
    [SerializeField] protected int maxBulletPerClipCount;
    [SerializeField] protected bool isLocked;

    public float Damage { get => damage; set => damage = value; }
    public float ReloadTime { get => reloadTime; set => reloadTime = value; }
    public float ShootingDelayTime { get => shootingDelayTime; set => shootingDelayTime = value; }
    public float BulletShootingSpeed { get => bulletShootingSpeed; set => bulletShootingSpeed = value; }
    public int MaxBulletCount { get => maxBulletCount; set => maxBulletCount = value; }
    public int MaxBulletPerClipCount { get => maxBulletPerClipCount; set => maxBulletPerClipCount = value; }
    public bool IsLocked { get => isLocked; set => isLocked = value; }
}
