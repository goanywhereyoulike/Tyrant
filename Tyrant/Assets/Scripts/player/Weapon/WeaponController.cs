using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public enum WeaponType
    {
        Cannon,
        Flamthrower,
        Laser,
        Matter,
        Mg,
        Pistol,
        Rocket,
        Shotgun,
        Spazer
    }

    [System.Serializable]
    public struct WeaponObject
    {
        public WeaponType weaponType;
        public Weapon weaponObject;
    }

    [SerializeField]
    private List<WeaponObject> weaponObjects = null;

    public Weapon CurrentWeapon { get; private set; }

    public void ChangeWeapon(int weaponIndex)
    {
        CurrentWeapon.gameObject.SetActive(false);
        CurrentWeapon = weaponObjects[weaponIndex].weaponObject;
        CurrentWeapon.gameObject.SetActive(true);
    }

    void Awake()
    {
        CurrentWeapon = weaponObjects[0].weaponObject;
    }

    void Update()
    {
    }
}
