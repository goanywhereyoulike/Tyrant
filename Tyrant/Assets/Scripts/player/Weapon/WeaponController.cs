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

        CurrentWeapon.ReloadUI.gameObject.SetActive(false);
        CurrentWeapon.weaponObject.gameObject.SetActive(false);
        CurrentWeapon = weaponObjects[weaponIndex];
        CurrentWeapon.weaponObject.gameObject.SetActive(true);
        CurrentWeapon.ReloadUI.gameObject.SetActive(true);

        //CurrentWeapon.weaponUI.gameObject.SetActive(true);

    }

    public void UnlockWeapon(int weaponIndex)
    {
        weaponObjects[weaponIndex].isUnlocked = true;
        weaponObjects[weaponIndex].weaponButton.gameObject.SetActive(true);
        //weaponObjects[0].isUnlocked = true;
    }

    void Start()
    {
        //CurrentWeapon.weaponObject = weaponObjects[0].weaponObject;
        
        //CurrentWeapon.weaponUI = weaponObjects[0].weaponUI;
       
        CurrentWeapon = weaponObjects[0];
        RoomManager.Instance.RoomChanged += DisableWeaponUI;
        //CurrentWeapon.weaponUI.gameObject.SetActive(true);

    }
    void DisableWeaponUI(int id)
    {
        if (RoomManager.Instance.RoomId != 1)
        {
            CurrentWeapon.ReloadUI.SetActive(false);
        }
    }
    void Update()
    {
    }
    
    
}
