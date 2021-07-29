using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerDisplay : MonoBehaviour
{
    // Start is called before the first frame update 
    public TowerTemplate tower;

    void Start()
    {
        gameObject.GetComponent<Tower>().Health = tower.health;
        gameObject.GetComponent<TowerShoot>().BulletLimit = tower.BulletLimit;
        gameObject.GetComponent<TowerShoot>().CoolDownSpeed = tower.CoolDownSpeed;
        gameObject.GetComponent<TowerShoot>().DistanceToShoot = tower.distanceToShoot;
        gameObject.GetComponent<TowerShoot>().FireRate = tower.fireRate;
        gameObject.GetComponent<TowerShoot>().BulletForce = tower.bulletForce;
        if (tower.type == "Basic")
        {
            //gameObject.GetComponent<TowerBullet>().bulletDamage = tower.bulletDamage;
        }
        if (tower.type == "CannonTower")
        {
            //gameObject.GetComponent<CannonTowerBullet>().bulletDamage = tower.bulletDamage;
        }


}

}
