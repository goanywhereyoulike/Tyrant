using System.Collections;
using UnityEngine;

public class NormalEnemy : Enemy
{
    [SerializeField]
    private EnemyUI enemyUi = null;

    [SerializeField]
    private Animator burningAnimator;

    // Start is called before the first frame update
    protected override void Start()
    {
        pathcount = 0;
        base.Start();
        enemyUi.MaxHealthChanged(EnemyState.MaxHealth);
        enemyUi.HealthChanged(EnemyState.MaxHealth);
        enemyUi.MaxArmorChanged(EnemyState.MaxArmor);
        enemyUi.ArmorChanged(EnemyState.MaxArmor);
    }

    // Update is called once per frame
    protected override void Update()
    {
        if(isDead)
        {
            enemyUi.HealthChanged(EnemyState.MaxHealth);
        }

        base.Update();
    }

    public override void TakeDamage(float damage)
    {
        Health -= damage;
        enemyUi.HealthChanged(Health);
    }

    public override void BurnArmor(float buringDamge)
    {
        armor -= buringDamge;
        enemyUi.ArmorChanged(armor);
        burningAnimator.gameObject.SetActive(true);
        burningAnimator.SetBool("Burning", true);
    }

    //------------------attck animation------------------------
    IEnumerator Attack()
    {
        Vector2 originalPosition = transform.position;

        Vector2 targetPosition = mTarget.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * EnemyState.AttackSpeed;

            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;

            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);

            yield return null;
        }
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
    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    if (collider.gameObject.name == mTarget.name)
    //    {
    //        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Tower" || collider.gameObject.tag == "Base")
    //        {
    //            IDamageable Targets = collider.gameObject.GetComponent<IDamageable>();
    //            if (Targets == null)
    //            {
    //                Targets = collider.gameObject.GetComponentInChildren<IDamageable>();
    //            }
    //            Targets.TakeDamage(damage);
    //            Debug.Log("attack");
    //        }
    //    }
    //}
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawWireSphere(transform.position, EnemyState.DetectRange);
        // Gizmos.DrawSphere(transform.position, enemyState.DetectRange);
    }

}
