using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTrap : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TrapTemplate TrapData;

    public GameObject explosionEffect;
    float countdown;
    private float delay = 0.0f;
    private float duration = 0.0f;
    private float radius = 0.0f;
    float shake = 0.0f;
    bool hasExploded = false;
    private float damage;
    [SerializeField]
    private Material matWhite;
    private Material matDefault;
    SpriteRenderer sr;
    void Start()
    {
        countdown = delay;
        sr = GetComponent<SpriteRenderer>();
        matDefault = sr.material;
        duration = TrapData.Duration;
        radius = TrapData.Range;
        damage = TrapData.TrapDamage;
    }

    void flash()
    {
        shake += Time.deltaTime;
        if (shake % 1 > duration)
        {
            sr.material = matWhite;
        }
        else
        {
            sr.material = matDefault;
        }


    }

    //public static void AddExplosionForce(Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force)
    //{
    //    var explosionDir = rb.position - explosionPosition;
    //    var explosionDistance = explosionDir.magnitude;

    //    // Normalize without computing magnitude again
    //    if (upwardsModifier == 0)
    //        explosionDir /= explosionDistance;
    //    else
    //    {
    //        // From Rigidbody.AddExplosionForce doc:
    //        // If you pass a non-zero value for the upwardsModifier parameter, the direction
    //        // will be modified by subtracting that value from the Y component of the centre point.
    //        explosionDir.y += upwardsModifier;
    //        explosionDir.Normalize();
    //    }

    //    rb.AddForce(Mathf.Lerp(0, explosionForce, (1 - explosionDistance)) * explosionDir, mode);
    //}
    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider2D[] collidiers = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D co in collidiers)
        {


            IDamageable ib = co.GetComponent<IDamageable>();
            Rigidbody2D rb = co.GetComponent<Rigidbody2D>();

            if (ib != null)
            {
                ib.TakeDamage(damage);
            }
            //if (rb)
            //{
            //    AddExplosionForce(rb,10.0f, transform.position, 5.0f,1.0f, ForceMode2D.Impulse);
            //}

        }


        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (countdown > 0.0f)
        {
            flash();
        }

        countdown -= Time.deltaTime;
        if (countdown <= 0.0f && !hasExploded)
        {
            Explode();
            hasExploded = true;


        }


    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(0, 1, 1, 1);
        Gizmos.DrawWireSphere(transform.position, radius);
        // Gizmos.DrawSphere(transform.position, enemyState.DetectRange);
    }

}
