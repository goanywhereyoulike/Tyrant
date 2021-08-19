using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{
    private ConnonStates connonStates = null;

    [SerializeField]private int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    protected override void Start()
    {
        base.Start();
        if (weaponInit)
        {
            return;
        }

        ObjectPoolManager.Instance.InstantiateObjects("ConnonBullet");
        ObjectPoolManager.Instance.InstantiateObjects("ConnonBulletEffect");

        connonStates = weaponStates as ConnonStates;
        weaponInit = true;

        currentAmmo = maxAmmo;
    }
    private void Update()
    {
        if (currentAmmo<=0)
        {
            Reload();
            return;
        }
    }
    void Reload()
    {
        Debug.Log("reloading");
        currentAmmo = maxAmmo;
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
            currentAmmo--;
        }
    }
}
