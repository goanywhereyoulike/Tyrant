using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="New Tower",menuName ="Towers/Basic")]
public class TowerTemplate : ScriptableObject
{
    public float health;
    public string type;
    public float distanceToShoot;
    public float fireRate;
    public float bulletForce;
    public float bulletDamage;
    public int price;
    public float BulletLimit;
    public float CoolDownSpeed;


}

