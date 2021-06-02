using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int playerHealth = 100;
    public Inventory mInventory { get; set; }
    public Weapon mWeapon { get; set; }

    void Start()
    {
        mInventory = GetComponent<Inventory>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (InputManager.Instance.GetKeyDown("healthpotion"))
        {
            HealthPotion healthPotion = mInventory.GetPickUp("health potion") as HealthPotion;
            if (healthPotion)
            {
                playerHealth += healthPotion.AddHealth;
                mInventory.DeletePickUp(healthPotion);
            }

        }
        Debug.Log(playerHealth);
    }
}
