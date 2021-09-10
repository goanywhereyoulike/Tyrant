using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBullet : MonoBehaviour
{

    public float bulletDamage = 5.0f;
    public GameObject hitEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        //Destroy(gameObject);

        if (enemy)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            enemy.TakeDamage(bulletDamage);
            gameObject.SetActive(false);
            Destroy(effect, 0.3f);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (collision.gameObject.tag == "Wall")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            gameObject.SetActive(false);
        }
        //Destroy(gameObject);

        if (enemy)
        {
            enemy.TakeDamage(bulletDamage);
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            gameObject.SetActive(false);

        }
        //Destroy(gameObject);
    }
}
