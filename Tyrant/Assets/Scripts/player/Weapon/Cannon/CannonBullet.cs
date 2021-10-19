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
        SplitBullet();

        //Enemy.TakeDamage(Damage);
        Vector3 force = Direction * PushForce;
        IPushable hitObject = Enemy.GetComponent<IPushable>();
        hitObject.BePushed(force);
    }

    private void Update()
    {
        BulletMoving();
    }

    private void SplitBullet()
    {
        for (int i = 1; i <= 6; i++)
        {
            var bullet = ObjectPoolManager.Instance.GetPooledObject("ConnonSplitBullet");
            if (bullet)
            {
                CannonSplitBullet cannonBullet = bullet.GetComponent<CannonSplitBullet>();
                float angle = i * 60 * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * Direction.x - Mathf.Sin(angle) * Direction.y;
                float y = Mathf.Sin(angle) * Direction.x + Mathf.Cos(angle) * Direction.y;
                Vector3 rotateDir = new Vector3(x, y, 0);
                cannonBullet.Direction = rotateDir.normalized;
                cannonBullet.BulletShootingSpeed = BulletShootingSpeed;
                cannonBullet.MovingRange = MovingRange * 0.3f;
                cannonBullet.StartPosition = gameObject.transform.position;
                cannonBullet.Damage = Damage;
                bullet.transform.position = gameObject.transform.position;
                bullet.SetActive(true);
            }
        }
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
