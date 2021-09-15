using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponUnlocker : PickUp
{
    private Player player;
    [SerializeField]
    private int weaponIndex;
    public void Interact(Player player)
    {
        player.GetComponentInChildren<WeaponController>().UnlockWeapon(weaponIndex);
        Destroy(gameObject);
    }
    protected override void Trigger2DEnter(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();
        }
    }
    private void Update()
    {
        if (CanBePicked && InputManager.Instance.GetKeyDown("Interact"))
        {
            Interact(player);
        }
    }


}


