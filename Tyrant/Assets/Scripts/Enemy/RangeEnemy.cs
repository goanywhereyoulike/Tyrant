using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
    // Start is called before the first frame update
    GameObject bObject;
    [SerializeField]
    private EnemyUI healthBar = null;
    protected override void Start()
    {
        ObjectPoolManager.Instance.InstantiateObjects("enemyBullet");
        base.Start();
        healthBar.MaxHealthChanged(EnemyState.MaxHealth);
        healthBar.HealthChanged(EnemyState.MaxHealth);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;
        healthBar.HealthChanged(Health);
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
            Vector3 direction = mTarget.position - transform.position;
            direction.Normalize();
            bObject = ObjectPoolManager.Instance.GetPooledObject("enemyBullet");
            bObject.transform.position = transform.position;
            bObject.GetComponent<EnemyBullet>().Position = mTarget.position;
            bObject.GetComponent<EnemyBullet>().Damage = (int)damage;
            bObject.GetComponent<EnemyBullet>().Range = stopDistance;
            bObject.GetComponent<EnemyBullet>().Direction = direction;
            bObject.SetActive(true);
        }
    }
}
