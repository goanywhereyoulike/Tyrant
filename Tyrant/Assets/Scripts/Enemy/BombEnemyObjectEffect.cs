using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemyObjectEffect : MonoBehaviour
{
    [SerializeField] private GameObject Obj;
    private Animator animator;
    [SerializeField]
    BombEnemy bombEnemy = new BombEnemy();

    //bool isEnter = false;
    private List<IDamageable> damageables = new List<IDamageable>();
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void DestroyedObject()
    {
        //isEnter = false;
        damageables.Clear();
        animator.gameObject.SetActive(false);
        Obj.GetComponent<BombEnemy>().Killed();
        //gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damageables.Contains(collision.gameObject.GetComponent<IDamageable>()))
        {
            return;
        }
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Tower"))
        {
            
            IDamageable damagebaleObejct = collision.gameObject.GetComponent<IDamageable>();
            if (damagebaleObejct != null)
            {
                damagebaleObejct.TakeDamage(bombEnemy.Damage);
                damageables.Add(damagebaleObejct);
            }
        }

        /*{
            if (collision.gameObject.tag == "Player")
            {
                Player player = collision.gameObject.GetComponent<Player>();
                player.TakeDamage(bombEnemy.Damage);
                isEnter = true;
            }

            if (collision.gameObject.tag == "Tower")
            {
                Tower tower = collision.gameObject.GetComponent<Tower>();
                tower.TakeDamage(bombEnemy.Damage);
                isEnter = true;
            }
        }*/
    }
}
