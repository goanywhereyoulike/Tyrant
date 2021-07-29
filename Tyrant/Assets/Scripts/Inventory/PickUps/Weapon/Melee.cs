using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : PickUp
{
    protected override void Trigger2DEnter(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            Inventory inventory = other.gameObject.GetComponent<Player>().MyInventory;
            inventory.AddPickUp(this);
            gameObject.SetActive(false);
        }
    }

}
