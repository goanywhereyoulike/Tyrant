using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{
    private ConnonStates connonStates = null;

    [SerializeField]private int maxAmmo = 5;
    private int currentAmmo;
    public float reloadTime = 1f;
    public GameObject[] ammoImages;
    protected override void Start()
    {
        base.Start();
        if (weaponInit)
        {
            return;
        }
        for (int i = 0; i <=0; i++)
        {
            ammoImages[i].gameObject.SetActive(false);
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
        if (InputManager.Instance.GetKeyDown("Reload"))
        {
            for (int i = 0; i <=4; i++)
            {
                ammoImages[i].gameObject.SetActive(true);
            }
            Debug.Log("reloading");
            currentAmmo = maxAmmo;
        }
    }
    public override void Fire()
    {
        base.Fire();

        if (canFire && currentAmmo > 0)
        {

            var bulletObject = ObjectPoolManager.Instance.GetPooledObject("ConnonBullet");
            if (bulletObject)
            {
                var bullet = bulletObject.GetComponent<CannonBullet>();
                bullet.BulletShootingSpeed = connonStates.BulletShootingSpeed;
                bullet.MovingRange = connonStates.ShootingRange;
                bullet.StartPosition = startShootingPointDict[Facing].transform.position;
                Vector2 different = InputManager.Instance.MouseWorldPosition - bullet.StartPosition;
                bullet.PushForce = connonStates.PushForce;
                bullet.Direction = different.normalized;
                bulletObject.transform.position = bullet.StartPosition;
                bulletObject.SetActive(true);
                AudioManager.instance.PlaySFX(0);
            }
            currentAmmo-=1;
            ammoImages[currentAmmo].gameObject.SetActive(false);
            //currentAmmo--;
        }
    }
}
