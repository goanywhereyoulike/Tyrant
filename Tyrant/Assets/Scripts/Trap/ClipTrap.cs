using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipTrap : MonoBehaviour
{
    public GameObject DestroyedTower;
    public int TrapNumber = 0;
    public float duration = 10.0f;
    PlayerMovement player;
    public Animator animator;
    private bool IsCounting = false;
    private float enemyMovespeed = 0.0f;
    Enemy enemy;
    //public Transform targetpos;

    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (IsCounting)
        {
            duration -= Time.deltaTime;
        }
        if (duration <= 0.0f)
        {
            Destroy(gameObject);
            if (enemy)
            {
                enemy.MoveSpeed = enemyMovespeed;
                enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }

        }//TowerToTarget(player.transform);
    }


    void Die()
    {
        Destroy(gameObject.transform.parent.gameObject);

       // Instantiate(DestroyedTower, transform.position, transform.rotation);

    }


    private void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (TrapNumber < 1)
        {
            string tag = collision.gameObject.tag;
            enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy)
            {
                enemyMovespeed = enemy.MoveSpeed;
                animator.SetTrigger("Enable");
                IsCounting = true;
                TrapNumber++;
                StartCoroutine(Trap(enemy));
                enemy.GetComponent<Rigidbody2D>().Sleep();
            }

        }

    }

    IEnumerator Trap(Enemy enemy)
    {

        yield return new WaitForSeconds(0.1f);

        enemy.MoveSpeed = 0.0f;
    
        enemy.transform.position = transform.position;

        enemy.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;


    }
}
