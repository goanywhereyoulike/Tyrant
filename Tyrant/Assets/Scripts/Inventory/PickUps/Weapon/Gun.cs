using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : PickUp
{
    public Transform firePosition;
    GameObject bullet;
    private Player player;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        ObjectPoolManager.Instance.InstantiateObjects("bullet");
    }
    private void Update()
    {
        if (InputManager.Instance.GetKeyDown("pick") && CanBePicked)
        {
            player.mInventory.AddPickUp(this);
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
            bullet.GetComponent<bullet>().direction = playerHeading;
        }

        //Instantiate(bullet, firePosition.position, firePosition.rotation);
    }
    
}
