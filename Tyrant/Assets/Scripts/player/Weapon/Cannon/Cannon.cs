using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{
    private ConnonStates connonStates = null;
    protected override void Awake()
    {
        base.Awake();

        ObjectPoolManager.Instance.InstantiateObjects("ConnonBullet");
        ObjectPoolManager.Instance.InstantiateObjects("ConnonBulletEffect");

        connonStates = weaponStates as ConnonStates;
    }

    public override void Fire()
    {
        base.Fire();

        if (canFire)
        {
            var bulletObject = ObjectPoolManager.Instance.GetPooledObject("ConnonBullet");
            if (bulletObject)
            {
                var bullet = bulletObject.GetComponent<CannonBullet>();
                bullet.Damage = connonStates.Damage;
                bullet.BulletShootingSpeed = connonStates.BulletShootingSpeed;
                bullet.MovingRange = connonStates.ShootingRange;
                bullet.StartPosition = startShootingPointDict[Facing].transform.position;
                Vector2 different = InputManager.Instance.MouseWorldPosition - bullet.StartPosition;
                bullet.Direction = different.normalized;
                bulletObject.transform.position = bullet.StartPosition;
                bulletObject.SetActive(true);
            }
        }
    }
}
