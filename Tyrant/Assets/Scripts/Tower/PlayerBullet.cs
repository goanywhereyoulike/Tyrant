using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public GameObject hitEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Tower")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
           // gameObject.SetActive(false);
            Destroy(gameObject);

            Tower tw = collision.gameObject.GetComponentInChildren<Tower>();
            if (tw)
            {
                tw.Health -= 5;
                tw.UpdateHealthBar(tw.Health);
            }
        }

    }
}
