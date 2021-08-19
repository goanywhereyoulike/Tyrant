using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleTrap : MonoBehaviour
{
    public GameObject GravityEffect;
    public float radius = 5.0f;
    public float forcemagnitude = 10.0f;
    public float duration = 10.0f;
    private GameObject effect;
    // Start is called before the first frame update
    void Start()
    {
        effect = Instantiate(GravityEffect, transform.position, transform.rotation);
        
        
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0.0f)
        {
            Destroy(effect);
            Destroy(gameObject);

        }
        else 
        {
            Attract();
        }
       
    }

    void Attract()
    {

        
        Collider2D[] collidiers = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D co in collidiers)
        {
            Vector3 direction = ( transform.position- co.gameObject.transform.position).normalized;
            Rigidbody2D rb = co.GetComponent<Rigidbody2D>();
            Enemy enemy = co.GetComponent<Enemy>();
            Vector2 force = direction * forcemagnitude;

            if (rb && enemy)
            {
                rb.AddForce(force,ForceMode2D.Force);
            }


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
