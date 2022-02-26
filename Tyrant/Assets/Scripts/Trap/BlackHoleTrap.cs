using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleTrap : MonoBehaviour
{
    [SerializeField]
    TrapTemplate TrapData;

    List<Enemy> mEnemies = new List<Enemy>();
    public GameObject GravityEffect;
    private float radius = 5.0f;
    private float forcemagnitude = 10.0f;
    private float duration = 10.0f;
    private GameObject effect;
    bool AddForce = false;
    // Start is called before the first frame update
    void Start()
    {
        effect = Instantiate(GravityEffect, transform.position, transform.rotation);
        radius = TrapData.Range;
        duration = TrapData.Duration;
        forcemagnitude = TrapData.TrapDamage;
    }

    // Update is called once per frame
    void Update()
    {
        duration -= Time.deltaTime;
        if (duration <= 0.0f)
        {
            List<Enemy> enemies = GameObjectsLocator.Instance.Get<Enemy>();
            foreach (var enemy in enemies)
            {
                if (enemy.gameObject.activeInHierarchy)
                {
                    enemy.gameObject.GetComponent<Rigidbody2D>().Sleep();
                }

            }
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

        AddForce = true;
        Collider2D[] collidiers = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (Collider2D co in collidiers)
        {
            if (co.GetComponent<Enemy>())
            {
                Enemy enemy = co.GetComponent<Enemy>();
                Vector3 direction = (transform.position - co.gameObject.transform.position).normalized;
                Rigidbody2D rb = co.GetComponent<Rigidbody2D>();
                Vector2 force = direction * forcemagnitude;

                if (rb && enemy)
                {
                    rb.AddForce(force, ForceMode2D.Force);
                }

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
