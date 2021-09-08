using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponUnlocker : MonoBehaviour,IInteractable
{
    private Player player;
    [SerializeField]
    private int weaponIndex;
    public void Interact(Player player)
    {
       //if(InputManager.Instance.GetKey("Interact"))
       //{
            player.GetComponentInChildren<WeaponController>().UnlockWeapon(weaponIndex);
            Destroy(gameObject);
       //}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();
            Interact(player);
        }
    }



}


