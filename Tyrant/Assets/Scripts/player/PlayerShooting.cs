using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private WeaponController weaponController = null;

    private void Update()
    {
        float holdingTime;
        if (InputManager.Instance.GetKey("Fire", out holdingTime))
        {
            weaponController.CurrentWeapon.weaponObject.HoldingFire(holdingTime);

        }

        if (InputManager.Instance.GetKey("Fire"))
        {
            weaponController.CurrentWeapon.weaponObject.Fire();

   
        }
        else if (InputManager.Instance.GetKeyUp("Fire"))
        {
            weaponController.CurrentWeapon.weaponObject.UnFire();
        }
    }
}
