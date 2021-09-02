using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChest : Chest
{
    [SerializeField]
    private GameObject weapon;
    [SerializeField]
    private Transform dropPosition;

    protected override void OpenChest()
    {
        GameObject item = Instantiate(weapon, dropPosition);
        
    }

}
