using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUps
{
    private PlayerMovement playerMovement;
    [SerializeField]
    private float increaseSpeed;
    // Start is called before the first frame update


    protected override void activeEffect()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        playerMovement = player.gameObject.GetComponent<PlayerMovement>();
        playerMovement.MoveSpeed += increaseSpeed;
    }
    protected override void deactiveEffect()
    {
        playerMovement.MoveSpeed -= increaseSpeed;
        //Destroy(gameObject);
    }
}
