using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{
    protected override void Start()
    {
        base.Start();

        ObjectPoolManager.Instance.InstantiateObjects("ConnonBullet");
        ObjectPoolManager.Instance.InstantiateObjects("ConnonBulletEffect");
    }

    public override void Fire()
    {
        base.Fire();

        if (canFire)
        {
            var bulletObject = ObjectPoolManager.Instance.GetPooledObject("ConnonBullet");
            if (bulletObject)
            {
                var bullet = bulletObject.GetComponent<Bullet>();
                bullet.Damage = weaponStates.Damage;
                bullet.BulletShootingSpeed = weaponStates.BulletShootingSpeed;
                bullet.MovingRange = weaponStates.ShootingRange;
                bullet.StartPosition = startShootingPointDict[Facing].transform.position;
                Vector2 different = InputManager.Instance.MouseWorldPosition - bullet.StartPosition;
                bullet.Direction = different.normalized;
                bulletObject.transform.position = bullet.StartPosition;
                bulletObject.SetActive(true);
            }
        }
    }
}
