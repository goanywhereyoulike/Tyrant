using System.Collections;
using UnityEngine;

public class NormalEnemy : Enemy
{
    public Material flashMaterial;
    private Material originalMaterial;


    [SerializeField]
    private EnemyUI enemyUi = null;

    private bool armorEnemy = false;

    // Start is called before the first frame update
    protected override void Start()
    {
        isPushable = true;
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

        originalMaterial = GetComponent<SpriteRenderer>().material;

    }

    // Update is called once per frame
    protected override void Update()
    {
        if(isDead)
        {
            GetComponent<SpriteRenderer>().material = originalMaterial;
            enemyUi.HealthChanged(EnemyState.MaxHealth);
        }

        base.Update();
    }

    public override void TakeDamage(float damage)
    {
        if (armor > 0)
            return;

        Health -= damage;
        StartCoroutine(Flash());
        enemyUi.HealthChanged(Health);
    }

    IEnumerator Flash()
    {
        GetComponent<SpriteRenderer>().material = flashMaterial;
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().material = originalMaterial;

    }
    public override void BurnArmor(float buringDamge)
    {
        //burningAnimator.gameObject.SetActive(true);
        //burningAnimator.SetBool("Burning", true);
        //if (!armorEnemy)
        //    return;

        //armor -= buringDamge;
        //enemyUi.ArmorChanged(armor);
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

    protected override void AttackBehavior()
    {
        base.AttackBehavior();
        StartCoroutine("Attack");
    }
}
