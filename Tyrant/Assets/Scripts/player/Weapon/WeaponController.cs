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
    public class WeaponObject
    {
        public WeaponType weaponType;
        public Weapon weaponObject;
        //public GameObject weaponUI;
        public GameObject ReloadUI;
        public bool isUnlocked;
        public WeaponWheelButton weaponButton;
    }

    [SerializeField]
    private List<WeaponObject> weaponObjects = null;
    public List<WeaponObject> WeaponObjects { get => weaponObjects; set => weaponObjects=value; }


    public WeaponObject CurrentWeapon;

    public void ChangeWeapon(int weaponIndex)
    {
        CurrentWeapon.weaponObject.gameObject.SetActive(false);
        //CurrentWeapon.weaponUI.gameObject.SetActive(false);
        //CurrentWeapon.weaponObject = weaponObjects[weaponIndex].weaponObject;
        //CurrentWeapon.weaponUI = weaponObjects[weaponIndex].weaponUI;
        CurrentWeapon.weaponType = weaponObjects[weaponIndex].weaponType;
        CurrentWeapon.weaponObject = weaponObjects[weaponIndex].weaponObject;
        CurrentWeapon.weaponObject.gameObject.SetActive(true);
        //CurrentWeapon.weaponUI.gameObject.SetActive(true);

    }

    public void UnlockWeapon(int weaponIndex)
    {
        weaponObjects[weaponIndex].isUnlocked = true;
        weaponObjects[weaponIndex].weaponButton.gameObject.SetActive(true);
        //weaponObjects[0].isUnlocked = true;
    }

    void Awake()
    {
        //CurrentWeapon.weaponObject = weaponObjects[0].weaponObject;
        
        //CurrentWeapon.weaponUI = weaponObjects[0].weaponUI;
        CurrentWeapon.weaponObject = weaponObjects[0].weaponObject;
        CurrentWeapon.weaponType = weaponObjects[0].weaponType;
        //CurrentWeapon.weaponUI.gameObject.SetActive(true);

    }

    void Update()
    {
    }
}
