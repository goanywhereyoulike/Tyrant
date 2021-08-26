using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPowerUp : PowerUps
{
    private WeaponController weaponController;
    private Laser laser;
    [SerializeField]
    private float reduceHold;
    private float originalHold;
    private void Start()
    {
        
    }
    protected override void activeEffect()
    {
        weaponController = player.GetComponentInChildren<WeaponController>();
        foreach (var item in weaponController.WeaponObjects)
        {
            if (item.weaponType == WeaponController.WeaponType.Laser)
            {
                laser = item.weaponObject.GetComponent<Laser>();
                break;
            }
        }
        originalHold = laser.ChargeTime;
        laser.ChargeTime -= reduceHold;
    }
    protected override void deactiveEffect()
    {
        laser.ChargeTime = originalHold;
    }
}
