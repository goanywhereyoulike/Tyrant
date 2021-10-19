using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangeEnemy : Enemy
{
    GameObject bObject;
    [SerializeField]
    private EnemyUI enemyUi = null;

  //  [SerializeField]
  //  private Animator BoomerangeAnimator = null;

    public float Damage { get => damage; set => damage = value; }
    bool firstshoot = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        ObjectPoolManager.Instance.InstantiateObjects("boomerangebullet");
        enemyUi.MaxHealthChanged(EnemyState.MaxHealth);
        enemyUi.HealthChanged(EnemyState.MaxHealth);
        //BoomerangeAnimator.gameObject.SetActive(false);
    }

    protected override void ReUse()
    {
        base.ReUse();
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

    public void Killed()
    {
        IsDead = true;
    }

    protected override void AttackBehavior()
    {
        base.AttackBehavior();
        // BoomerangeAnimator.gameObject.SetActive(true);
        if (mTarget != null)
        {
            Vector3 direction = mTarget.position - transform.position;
            direction.Normalize();
            //if (!create)
            //{
            //
            //}

            if(!firstshoot)
            {
                bObject = ObjectPoolManager.Instance.GetPooledObject("boomerangebullet");
                bObject.transform.position = transform.position;
                var bo = bObject.GetComponent<Boomerange>();
                bo.ShootPosition = transform.position;
                bo.Position = mTarget.position;
                bo.Damage = (int)damage;
                bo.Range = stopDistance;
                bo.Direction = direction;
                bObject.SetActive(true);
                firstshoot = true;
            }

            if (bObject.activeInHierarchy == false)
            {
                firstshoot = false;
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        if (armor > 0)
            return;

        Health -= damage;
        enemyUi.HealthChanged(Health);
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawWireSphere(transform.position, EnemyState.DetectRange);
        // Gizmos.DrawSphere(transform.position, enemyState.DetectRange);
    }
}
