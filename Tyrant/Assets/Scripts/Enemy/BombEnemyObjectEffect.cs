using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemyObjectEffect : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    private Animator animator;
    [SerializeField]
    BombEnemy bombEnemy = new BombEnemy();

    bool isEnter = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void DestroyedObject()
    {
        isEnter = false;
        animator.gameObject.SetActive(false);
        gameObject.GetComponent<BombEnemy>().Killed();
        //gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isEnter)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            player.TakeDamage(bombEnemy.Damage);
            isEnter = true;
        }

        
    }
}
