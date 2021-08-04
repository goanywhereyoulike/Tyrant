using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : Bullet
{
    [SerializeField]
    private Animator animator = null;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

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

    protected override void OnhitEffect(Vector3 objectPosition)
    {
        base.OnhitEffect();
        var effectObject = ObjectPoolManager.Instance.GetPooledObject("LaserBulletEffect");
        if (effectObject)
        {
            effectObject.transform.position = objectPosition;
            effectObject.SetActive(true);
        }
    }

    protected override void OnHit(IDamageable Enemy)
    {
        base.OnHit(Enemy);

        Enemy.TakeDamage(Damage);
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
            IDamageable Enemy = collision.gameObject.GetComponent<IDamageable>();
            OnHit(Enemy);
            OnhitEffect(collision.gameObject.transform.position);
        }
    }
}
