using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : PickUp
{
    [SerializeField]
    private int value;
    private Player player;
    // Start is called before the first frame update
    protected override void Trigger2DEnter(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject.GetComponent<Player>();
            player.coin += value;
            gameObject.SetActive(false);
        }
    }
}
