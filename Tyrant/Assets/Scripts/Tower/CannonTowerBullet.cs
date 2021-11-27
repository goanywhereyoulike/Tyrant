using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonTowerBullet : MonoBehaviour
{
    [SerializeField]
    private float wDetectRange;
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
    //    Debug.DrawLine(Vector2.zero, hit.point, Color.yellow, 10.0f);
    //    PSC levelBoss = hit.collider.gameObject.GetComponent<PSC>();
    //    if (levelBoss)
    //    {

    //        //if (levelBoss && hit.distance < 0.01f)
    //        //{
    //        //    levelBoss.TakeDamage(bulletDamage);
    //        //    Vector2 reflexAngle = Vector2.Reflect(lastVelocity, hit.normal);
    //        //    if (rb)
    //        //    {
    //        //        rb.velocity = reflexAngle.normalized * lastVelocity.magnitude * 0.5f;
    //        //    }
    //        //    realHittime++;

    //        //}

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
            //if (hit.collider.gameObject.GetComponent<PSC>())
            //{
            if (rb)
            {
                rb.velocity = lastVelocity * (-0.5f);
            }
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
            if (!checkWall(enemy))
            {
                hitObject.BePushed(force);

            }
           
            
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

    bool checkWall(Enemy enemy)
    {
        var erb = enemy.GetComponent<Rigidbody2D>();
        var etrans = enemy.gameObject.transform;

        RaycastHit2D upHit = Physics2D.Raycast(etrans.position, Vector2.up, wDetectRange);
        if (upHit.collider != null && upHit.collider.tag == "Wall")
        {
            return true;
        }
        //Debug.DrawRay(transform.position, Vector2.up, Color.green, 2);

        RaycastHit2D downHit = Physics2D.Raycast(etrans.position, Vector2.down, wDetectRange);
        if (downHit.collider != null && downHit.collider.tag == "Wall")
        {
            return true;
        }
        //Debug.DrawRay(transform.position, Vector2.down, Color.green, 2);
        RaycastHit2D rightHit = Physics2D.Raycast(etrans.position, Vector2.right, wDetectRange);
        if (rightHit.collider != null && rightHit.collider.tag == "Wall")
        {
            return true;
        }
        //Debug.DrawRay(transform.position, Vector2.left, Color.green, 2);
        RaycastHit2D leftHit = Physics2D.Raycast(etrans.position, Vector2.left, wDetectRange);
        if (leftHit.collider != null && leftHit.collider.tag == "Wall")
        {
            return true;
        }

        return false;
    }


}
