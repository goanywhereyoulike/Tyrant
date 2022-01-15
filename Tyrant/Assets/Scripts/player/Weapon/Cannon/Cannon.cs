using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{
    private ConnonStates connonStates = null;
    private int currentAmmo;

    [SerializeField]
    private float reloadTime = 0.5f;

    private bool isReloading = false;
    public GameObject reloadUI;
    public GameObject[] ammoImages;
    protected override void Start()
    {
        reloadUI.SetActive(false);
        base.Start();
        if (weaponInit)
        {
            return;
        }
        /*for (int i = 0; i <=0; i++)
        {
            ammoImages[i].gameObject.SetActive(false);
        }*/
        ObjectPoolManager.Instance.InstantiateObjects("ConnonBullet");
        ObjectPoolManager.Instance.InstantiateObjects("ConnonSplitBullet");
        ObjectPoolManager.Instance.InstantiateObjects("ConnonBulletEffect");

        connonStates = weaponStates as ConnonStates;
        weaponInit = true;

        currentAmmo = connonStates.MaxAmmo;

        durability = int.MaxValue;
    }
    private void Update()
    {
        if (!isReloading && (currentAmmo <= 4 && InputManager.Instance.GetKeyDown("Reload") || currentAmmo == 0))
        {
            //AudioManager.instance.PlaySFX(12);
            StartCoroutine(Reload());
            return;
        }
    }
    IEnumerator Reload()
    {
        //canFire = false;
        //if ()
        //{
        isReloading = true;

        reloadUI.SetActive(true);
        yield return new WaitForSeconds(reloadTime);
        for (int i = 0; i <= 4; i++)
        {
            ammoImages[i].gameObject.SetActive(true);
        }
        Debug.Log("reloading");
        currentAmmo = connonStates.MaxAmmo;
        reloadUI.SetActive(false);
        isReloading = false;
     
        //canFire = true;
        //}
    }
    public override void Fire()
    {
        base.Fire();
        if(isReloading)
        {
            return;
        }
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
                bullet.Damage = connonStates.Damage;
                bulletObject.transform.position = bullet.StartPosition;
                bulletObject.SetActive(true);
                //AudioManager.instance.PlaySFX(0);
            }
            currentAmmo -= 1;
            ammoImages[currentAmmo].gameObject.SetActive(false);
            //currentAmmo--;
        }
    }

   public override void PlayOneTimeFireSound()
   {
        if(currentAmmo <= 0)
        {
            //AudioManager.instance.PlaySFX(11);
        }
   }
}
