using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : PickUp
{
    public Transform firePosition;
    GameObject bullet;
    private void Start()
    {
        ObjectPoolManager.Instance.InstantiateObjects("bullet");
    }
   
    protected override void Trigger2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            Inventory inventory = other.gameObject.GetComponent<Player>().mInventory;
            inventory.AddPickUp(this);
            gameObject.SetActive(false);
        }
    }
    public void Fire(Vector2 playerHeading)
    {
        
        bullet = ObjectPoolManager.Instance.GetPooledObject("bullet");
        if(bullet)
        {
            bullet.SetActive(true);
            bullet.transform.position = firePosition.position;
            bullet.GetComponent<bullet>().Direction = playerHeading;
        }

        //Instantiate(bullet, firePosition.position, firePosition.rotation);
    }
    
}
