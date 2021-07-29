using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private WeaponController weaponController = null;

    private void Update()
    {
        if (InputManager.Instance.GetKey("Fire"))
        {
            weaponController.CurrentWeapon.Fire();
        }
    }
}
