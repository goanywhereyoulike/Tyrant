using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponUnlocker : PickUp
{
    private Player player;
    [SerializeField]
    private int weaponIndex;
    [SerializeField]
    private bool isUnlimited;
    public void Interact(Player player)
    {
        player.GetComponentInChildren<WeaponController>().ChangeWeapon(weaponIndex);
        if (!isUnlimited)
            Destroy(gameObject);
    }
    protected override void Trigger2DEnter(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();
            player.InteractButton.GetComponent<Image>().enabled = true;
        }
    }
    protected override void Trigger2DExit(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player.InteractButton.GetComponent<Image>().enabled = false;
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


