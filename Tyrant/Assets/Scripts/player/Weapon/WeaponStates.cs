using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeapoinStates", menuName = "Player/Weapon", order = 2)]
public class WeaponStates : ScriptableObject
{
    [SerializeField] private float damage;
    [SerializeField] private float shootingRange;
    [SerializeField] private float reloadTime;
    [SerializeField] private float shootingDelayTime;
    [SerializeField] private float bulletShootingSpeed;
    [SerializeField] private int maxBulletCount;
    [SerializeField] private int maxBulletPerClipCount;
    [SerializeField] private bool isLocked;

    public float Damage { get => damage; set => damage = value; }
    public float ShootingRange { get => shootingRange; set => shootingRange = value; }
    public float ReloadTime { get => reloadTime; set => reloadTime = value; }
    public float ShootingDelayTime { get => shootingDelayTime; set => shootingDelayTime = value; }
    public float BulletShootingSpeed { get => bulletShootingSpeed; set => bulletShootingSpeed = value; }
    public int MaxBulletCount { get => maxBulletCount; set => maxBulletCount = value; }
    public int MaxBulletPerClipCount { get => maxBulletPerClipCount; set => maxBulletPerClipCount = value; }
    public bool IsLocked { get => isLocked; set => isLocked = value; }
}
