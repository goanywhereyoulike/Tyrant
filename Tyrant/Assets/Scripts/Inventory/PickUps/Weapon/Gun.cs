using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : PickUp
{
  
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
    
}
