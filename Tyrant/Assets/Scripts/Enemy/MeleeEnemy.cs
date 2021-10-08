using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    [SerializeField]
    private EnemyUI enemyUi = null;

    //  [SerializeField]
    //  private Animator AttackAnimator = null;

    private bool armorEnemy = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        enemyUi.MaxHealthChanged(EnemyState.MaxHealth);
        enemyUi.HealthChanged(EnemyState.MaxHealth);
        armorEnemy = EnemyState.MaxArmor > 0;
        if (armorEnemy)
        {
            enemyUi.MaxArmorChanged(EnemyState.MaxArmor);
            enemyUi.ArmorChanged(EnemyState.MaxArmor);
        }
        else
        {
            enemyUi.ShutdownArmorBar();
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isDead)
        {
            enemyUi.HealthChanged(EnemyState.MaxHealth);
        }

        base.Update();
    }

    public override void TakeDamage(float damage)
    {
        if (armor > 0)
            return;

        Health -= damage;
        enemyUi.HealthChanged(Health);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (mTarget != null)
        {
            if (collision.gameObject.name == mTarget.name)
            {
                if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Tower" || collision.gameObject.tag == "Base")
                {
                    IDamageable Targets = collision.gameObject.GetComponent<IDamageable>();
                    if (Targets == null)
                    {
                        Targets = collision.gameObject.GetComponentInChildren<IDamageable>();
                    }
                    Targets.TakeDamage(damage);
                    Debug.Log("attack");
                }
            }
        }
    }
   
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawWireSphere(transform.position, EnemyState.DetectRange);
        // Gizmos.DrawSphere(transform.position, enemyState.DetectRange);
    }

    protected override void AttackBehavior()
    {
        base.AttackBehavior();
    }
}
