using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonSplitBullet : Bullet
{
    public float MovingRange { get; set; }
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

        Enemy.GetComponent<Enemy>().TakeDamage(Damage);
        //IPushable hitObject = Enemy.GetComponent<IPushable>();
    }

    private void Update()
    {
        BulletMoving();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            OnHit(collision.gameObject);

        }

        if (collision.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "Boss")
        {
            //Debug.Log(damage);
            //gameObject.SetActive(false);
        }
    }

}
