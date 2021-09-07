using UnityEngine;

public class BossBullet : MonoBehaviour
{
    private float damage;
    private float damagetoTower;
    private float damagetoCannonTower;
    private float damagetoChainTower;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Tower")
        {
            IDamageable Targets = collision.gameObject.GetComponent<IDamageable>();
            if (Targets == null)
            {
                Targets = collision.gameObject.GetComponentInChildren<IDamageable>();
            }
            Targets.TakeDamage(damagetoTower);
        }
        if(collision.gameObject.tag == "ChainTower")
        {
            IDamageable Targets = collision.gameObject.GetComponent<IDamageable>();
            if (Targets == null)
            {
                Targets = collision.gameObject.GetComponentInChildren<IDamageable>();
            }
            Targets.TakeDamage(damagetoChainTower);
        }
        if (collision.gameObject.tag == "CannonTower")
        {
            IDamageable Targets = collision.gameObject.GetComponent<IDamageable>();
            if (Targets == null)
            {
                Targets = collision.gameObject.GetComponentInChildren<IDamageable>();
            }
            Targets.TakeDamage(damagetoCannonTower);
        }
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Tower" || collision.gameObject.tag == "Base")
        {
            IDamageable Targets = collision.gameObject.GetComponent<IDamageable>();
            if (Targets == null)
            {
                Targets = collision.gameObject.GetComponentInChildren<IDamageable>();
            }
            Targets.TakeDamage(damage);
        }
    }
}
