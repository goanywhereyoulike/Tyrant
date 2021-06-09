using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{
    [SerializeField]
    float bulletDamage = 5.0f;
    public GameObject hitEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (tag == "Player" || tag == "Wall" || tag == "Tower")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);

            gameObject.SetActive(false);
            //Destroy(gameObject);
        }

        if (enemy)
        {
            enemy.TakeDamage(bulletDamage);
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            gameObject.SetActive(false);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.3f);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
