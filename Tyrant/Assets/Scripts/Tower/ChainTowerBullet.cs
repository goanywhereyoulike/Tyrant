using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainTowerBullet : MonoBehaviour
{
    ChainTowerShoot ts;
    public float bulletDamage = 5.0f;
    public GameObject hitEffect;
    private Rigidbody2D rb;
    public bool IsFired = false;
    Vector3 lastVelocity;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ts = GetComponentInParent<ChainTowerShoot>();
        IsFired = false;
    }

    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    void FixedUpdate()
    {
        if (ts && rb)
        {
            Vector3 fp = ts.gameObject.transform.position - transform.position;
            fp = fp.normalized * rb.mass * 1.0f;
            rb.AddForce(fp, ForceMode2D.Force);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Enemy")
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
                Destroy(effect, 0.3f);
                gameObject.SetActive(false);
                IsFired = false;
                enemy.TakeDamage(bulletDamage);
                ts.TotalBulletDecrease();
            }

        }
        if (tag == "Boss")
        {
            PSC levelBoss = collision.gameObject.GetComponent<PSC>();
            levelBoss.TakeDamage(bulletDamage);
            ts.TotalBulletDecrease();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();


        if (enemy)
        {
            enemy.TakeDamage(bulletDamage);
        }
        if (tag == "Boss")
        {
            PSC levelBoss = collision.gameObject.GetComponent<PSC>();
            levelBoss.TakeDamage(bulletDamage);

        }

        if (tag != "Chain" && tag != "Tower")
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            ts.TotalBulletDecrease();
            // gameObject.SetActive(false);
            gameObject.transform.parent.gameObject.transform.parent = null;
            gameObject.transform.parent.gameObject.SetActive(false);
            IsFired = false;
        }


        //Destroy(gameObject)
    }
}
