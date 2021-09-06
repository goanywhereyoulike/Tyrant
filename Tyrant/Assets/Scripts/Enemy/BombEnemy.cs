using System.Collections;
using UnityEngine;

public class BombEnemy : Enemy
{
    [SerializeField]
    private EnemyUI enemyUi = null;

    [SerializeField]
    private Animator bombAnimator = null;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        enemyUi.MaxHealthChanged(EnemyState.MaxHealth);
        enemyUi.HealthChanged(EnemyState.MaxHealth);
        bombAnimator.gameObject.SetActive(false);
    }

    protected override void ReUse()
    {
        base.ReUse();
        StartCoroutine(HealthDecay());
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isDead)
        {
            enemyUi.HealthChanged(EnemyState.MaxHealth);
        }

        base.Update();

        MoveSpeed = (2 * EnemyState.MaxHealth - Health) * 4;
    }

    IEnumerator HealthDecay()
    {
        while (!IsDead)
        {
            yield return new WaitForSeconds(0.5f);
            if (Health > 0.1)
                TakeDamage(0.1f);
        }
    }

    public void Killed()
    {
        IsDead = true;
    }

    protected override void AttackBehavior()
    {
        base.AttackBehavior();
        bombAnimator.gameObject.SetActive(true);
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

