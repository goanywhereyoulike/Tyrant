using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : Bullet
{
    public float MovingRange { get; set; }
    public float PushForce { get; set; }
    protected override void BulletMoving()
    {
        base.BulletMoving();

        transform.position += Direction * BulletShootingSpeed * Time.deltaTime;
        if ((transform.position - StartPosition).magnitude >= MovingRange)
        {
            //Debug.Log("destoryed bullets");
            gameObject.SetActive(false);
        }
    }

    protected override void OnhitEffect()
    {
        base.OnhitEffect();
        var effectObject = ObjectPoolManager.Instance.GetPooledObject("ConnonBulletEffect");
        if (effectObject)
        {
            effectObject.transform.position = transform.position;
            effectObject.SetActive(true);
        }
    }

    protected override void OnHit(GameObject Enemy)
    {
        base.OnHit(Enemy);

        //Enemy.TakeDamage(Damage);
        Vector3 force = Direction * PushForce;
        IPushable hitObject = Enemy.GetComponent<IPushable>();
        hitObject.BePushed(force);
    }

    private void Update()
    {
        BulletMoving();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //Debug.Log(damage);
            OnHit(collision.gameObject);
            OnhitEffect();
            gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Boss")
        {
            //Debug.Log(damage);
            OnHit(collision.gameObject);
            OnhitEffect();
            gameObject.SetActive(false);
        }
    }


}
