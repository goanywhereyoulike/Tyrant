using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xRangeEnemy : Enemy
{
    GameObject bObject;
    [SerializeField]
    private EnemyUI enemyUi = null;

    //  [SerializeField]
    //  private Animator AttackAnimator = null;

    private bool armorEnemy = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ObjectPoolManager.Instance.InstantiateObjects("xbullet");
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
        if (mTarget != null)
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 direction =  mTarget.position - transform.position;
                direction.Normalize();
                bObject = ObjectPoolManager.Instance.GetPooledObject("xbullet");
                bObject.transform.position = transform.position;
                float angle = 60 * Mathf.Deg2Rad;
                if (i==2)
                {
                    angle *= -1;
                }
                else
                {
                    angle *= i;
                }
                float x = Mathf.Cos(angle) * direction.x - Mathf.Sin(angle) * direction.y;
                float y = Mathf.Sin(angle) * direction.x + Mathf.Cos(angle) * direction.y;
                Vector3 rotateDir = new Vector3(x, y, 0);
                var bo = bObject.GetComponent<XBullet>();
                bo.StartPosition = transform.position;
                bo.Direction = rotateDir.normalized;
                bo.Damage = (int)damage;
                bo.Range = stopDistance;
                bObject.SetActive(true);
            }
        }
    }
}
