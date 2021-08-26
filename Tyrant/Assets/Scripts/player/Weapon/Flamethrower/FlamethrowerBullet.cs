using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamethrowerBullet : Bullet
{
    //public float StartTime { get; set; }
    //public float MovingDuration { get; set; }
    //protected override void BulletMoving()
    //{
    //    base.BulletMoving();

    //    transform.position += Direction * BulletShootingSpeed * Time.deltaTime;
    //    if (StartTime + MovingDuration <= Time.time)
    //    {
    //        //Debug.Log("destoryed bullets");
    //        gameObject.SetActive(false);
    //    }
    //}

    //protected override void OnhitEffect()
    //{
    //    base.OnhitEffect();
    //    var effectObject = ObjectPoolManager.Instance.GetPooledObject("ConnonBulletEffect");
    //    if (effectObject)
    //    {
    //        effectObject.transform.position = transform.position;
    //        effectObject.SetActive(true);
    //    }
    //}

    //protected override void OnHit(IDamageable Enemy)
    //{
    //    base.OnHit(Enemy);

    //    Enemy.TakeDamage(Damage);
    //}

    //private void Update()
    //{
    //    BulletMoving();
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        //Debug.Log(damage);
    //        IDamageable Enemy = collision.gameObject.GetComponent<IDamageable>();
    //        OnHit(Enemy);
    //        OnhitEffect();
    //        gameObject.SetActive(false);
    //    }
    //}

    public float BurnDamage { get; set; }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<Enemy>().BurnArmor(BurnDamage);
        }
    }
}
