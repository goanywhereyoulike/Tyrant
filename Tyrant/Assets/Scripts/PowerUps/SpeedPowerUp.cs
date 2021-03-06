using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUps
{
    private PlayerMovement playerMovement;
    [SerializeField]
    private float increaseSpeed;
    //private float originalSpeed;

    protected override void activeEffect()
    {
        isTriggered = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        //originalSpeed = playerMovement.MoveSpeed;
        playerMovement.MoveSpeed += increaseSpeed;
    }
    protected override void deactiveEffect()
    {
        playerMovement.MoveSpeed -= increaseSpeed;
    }
}
