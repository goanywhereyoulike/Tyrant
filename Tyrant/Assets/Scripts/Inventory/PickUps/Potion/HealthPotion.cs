using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : PickUp
{
    [SerializeField]
    private int addHealth;

    public int AddHealth { get => addHealth; set => addHealth = value; }
    
    protected override void Trigger2D(Collider2D collision)
    {
        if (collision != null)
        { }
        base.Trigger2D(collision);
        if (collision.gameObject.tag == "Player")
        {
            Inventory inventory = collision.gameObject.GetComponent<Player>().mInventory;
            inventory.AddPickUp(this);
            gameObject.SetActive(false);
        }
    }
}
