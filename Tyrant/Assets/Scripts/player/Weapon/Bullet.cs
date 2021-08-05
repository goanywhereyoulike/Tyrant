using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    public float Damage { get; set; }
    public float BulletShootingSpeed { get; set; }
    public Vector3 Direction { get; set; }
    public Vector3 StartPosition { get; set; }
    protected virtual void BulletMoving()
    {

    }

    protected virtual void OnhitEffect()
    {

    }

    protected virtual void OnhitEffect(Vector3 objectPosition)
    {

    }

    protected virtual void OnHit(IDamageable Enemy)
    {

    }
}
