using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTowerBullet : MonoBehaviour
{
    public float PushForce = 10.0f;
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

    //private void Update()
    //{

    //    RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward);

        
    //    if (hit.collider)
    //    {
    //        PSC levelBoss = hit.collider.gameObject.GetComponentInChildren<PSC>();
    //        if (levelBoss && hit.distance<0.01f)
    //        {
    //            levelBoss.TakeDamage(bulletDamage);
    //            Vector2 reflexAngle = Vector2.Reflect(lastVelocity, hit.normal);
    //            if (rb)
    //            {
    //                rb.velocity = reflexAngle.normalized * lastVelocity.magnitude * 0.5f;
    //            }
    //            realHittime++;

    //        }

    //    }
    //    if (realHittime >= AllHittime)
    //    {
    //        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
    //        Destroy(effect, 0.3f);
    //        gameObject.SetActive(false);
    //        realHittime = 0;
    //    }
    //}
    private void LateUpdate()
    {
        lastVelocity = rb.velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        PSC levelBoss = collision.gameObject.GetComponent<PSC>();
        if (levelBoss)
        {
            levelBoss.TakeDamage(bulletDamage);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward);
            Debug.DrawLine(Vector2.zero, hit.point, Color.yellow, 10.0f);
            //if (hit.collider.gameObject.GetComponent<PSC>())
            //{
            //    Vector2 reflexAngle = Vector2.Reflect(lastVelocity, hit.normal);
            //    if (rb)
            //    {
            //        rb.velocity = reflexAngle.normalized * lastVelocity.magnitude * 0.5f;
            //    }

                
            //}
            realHittime++;
        }
        if (realHittime >= AllHittime)
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, 0.3f);
            gameObject.SetActive(false);
            realHittime = 0;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        PSC levelBoss = collision.gameObject.GetComponent<PSC>();
        if (tag == "Player" || tag == "Wall" || tag == "Base" || tag == "Enemy")
        {
            Vector2 reflexAngle = Vector2.Reflect(lastVelocity, collision.contacts[0].normal);
            if (rb)
            {
                rb.velocity = reflexAngle.normalized * lastVelocity.magnitude * 0.5f;
            }
            Rigidbody2D crb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (crb)
            {
                crb.velocity = new Vector2(0, 0);
            }

            realHittime++;
        }

        if (enemy)
        {
            enemy.TakeDamage(bulletDamage);
            Vector3 direction = (collision.gameObject.transform.position - transform.position).normalized;
            Vector3 force = direction * PushForce;
            IPushable hitObject = enemy.GetComponent<IPushable>();
            hitObject.BePushed(force);
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
