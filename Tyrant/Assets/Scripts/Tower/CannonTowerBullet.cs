using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTowerBullet : MonoBehaviour
{
    public float bulletDamage = 5.0f;
    public GameObject hitEffect;
    public int AllHittime = 3;
    private int realHittime = 0;
    private Rigidbody2D rb;
    Vector3 lastVelocity;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        AllHittime = 3;
        realHittime = 0;
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Player" || tag == "Wall" || tag == "Tower" || tag == "Base" || tag == "Enemy")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            gameObject.SetActive(false);
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        PSC levelBoss = collision.gameObject.GetComponent<PSC>();

        realHittime++;
        if (enemy)
        {
            enemy.TakeDamage(bulletDamage);
        }
        if (levelBoss)
        {
            levelBoss.TakeDamage(bulletDamage);
        }
        if (realHittime >= AllHittime)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            gameObject.SetActive(false);
            realHittime = 0;
        }
        //Destroy(gameObject)
    }
}
