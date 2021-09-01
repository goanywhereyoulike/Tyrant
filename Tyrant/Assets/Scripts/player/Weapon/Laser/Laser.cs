using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : Weapon
{
    [SerializeField]
    private Animator animator;

    private LaserStates laserStates = null;
    
    private bool charged = false;
    private bool charging = true;

    public Slider chargingBar;
    //private float currentChargingTime;
   
    protected override void Start()
    {
        base.Start();
        if (weaponInit)
        {
            return;
        }
        
        chargingBar.gameObject.SetActive(false);
        laserStates = weaponStates as LaserStates;
        chargingBar.maxValue = laserStates.HoldingTime;
        ObjectPoolManager.Instance.InstantiateObjects("LaserBullet");
        ObjectPoolManager.Instance.InstantiateObjects("LaserBulletEffect");
        weaponInit = true;
    }

    public override void UnFire()
    {
        base.UnFire();

        charging = false;
        animator.SetBool("Charging", charging);
        chargingBar.value = 0f;

        if (charged)
        {
            var bulletObject = ObjectPoolManager.Instance.GetPooledObject("LaserBullet");
            if (bulletObject)
            {
                var bullet = bulletObject.GetComponent<LaserBullet>();
                bullet.ForzenTime = laserStates.FrozenTime;
                bullet.FrozenSpeed = laserStates.FrozenSpeed;
                bullet.BulletShootingSpeed = laserStates.BulletShootingSpeed;
                bullet.MovingRange = laserStates.ShootingRange;
                bullet.StartPosition = startShootingPointDict[Facing].transform.position;
                Vector2 different = InputManager.Instance.MouseWorldPosition - bullet.StartPosition;
                bullet.Direction = different.normalized;
                bulletObject.transform.position = bullet.StartPosition;
                Vector3 vecDir = InputManager.Instance.MouseWorldPosition - transform.position;
                bulletObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f - Mathf.Atan2(-vecDir.y, vecDir.x) * Mathf.Rad2Deg);
                bulletObject.SetActive(true);
            }
            charged = false;
        }
    }

    public override void HoldingFire(float holdingTime)
    {
        base.HoldingFire(holdingTime);
        chargingBar.gameObject.SetActive(true);
        if (holdingTime >= laserStates.HoldingTime)
        {
            charged = true;
        }

        chargingBar.value = holdingTime;
        Vector3 vecDir = InputManager.Instance.MouseWorldPosition - transform.position;
        animator.gameObject.transform.eulerAngles = new Vector3(0.0f, 0.0f, 180.0f - Mathf.Atan2(-vecDir.y, vecDir.x) * Mathf.Rad2Deg);
        animator.gameObject.transform.position = startShootingPointDict[Facing].transform.position;

        charging = true;
        animator.SetBool("Charging", charging);
    }
}
