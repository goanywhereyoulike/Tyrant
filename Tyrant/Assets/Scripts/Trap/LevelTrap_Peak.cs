using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrap_Peak : MonoBehaviour
{
    // Start is called before the first frame update
    Player player;
    public float damage = 1.0f;
    bool IsDamaged = false;
    float WaitFire = 0.0f;
    public float FireRate = 0.1f;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       
        if (IsDamaged)
        {
            WaitFire += Time.deltaTime;
            if (WaitFire > FireRate)
            {
                if (player)
                {
                    player.TakeDamage(damage);
                }
                WaitFire = 0.0f;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<Player>();
        string tag = collision.gameObject.tag;
        if (player)
        {

            //player.TakeDamage(damage);
            IsDamaged = true;

        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        string tag = collision.gameObject.tag;
        if (player)
        {
            IsDamaged = false;

        }


    }

}
