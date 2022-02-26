using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    public enum AttackBehaviors
    {
        normal,
        heavy,
        rotation,
        destroy,
        summon
    }

    #region Golem Attritube
    [Header("Property:")]
    [SerializeField]
    private float health = 0.0f;
    [SerializeField]
    private float moveSpeed = 0.0f;

    private Player targetObejct = null;
    private SpriteRenderer golemSprite = null;
    private Animator golemAnimator = null;
    private bool isMoving = false;
    private bool canMove = true;

    public AttackBehaviors Behaviors { get; set; }
    public float Health { get => health; set => health = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    #endregion

    #region NormalAttack
    [Header("Normal Attack Attribute:")]
    [SerializeField]
    private float normalAttackRange = 0.0f;
    [SerializeField]
    private float normalAttackCooldown = 0.0f;

    private float normalAttackTimer = 0.0f;
    #endregion

    #region Heavy Attack
    [Header("Heavy Attack Attribute:")]
    [SerializeField]
    private float heavyAttackRange = 0.0f;
    [SerializeField]
    private float heavyAttackCooldown = 0.0f;
    [SerializeField]
    private GameObject heavyAttackEffectObject = null;

    private float heavyAttackEffectOffset = 2f;
    private float heavyAttackTimer = 0.0f;
    #endregion

    private void Start()
    {
        Behaviors = AttackBehaviors.heavy;
        normalAttackTimer = normalAttackCooldown;
        heavyAttackTimer = heavyAttackCooldown;
        golemSprite = GetComponent<SpriteRenderer>();
        golemAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!targetObejct)
            targetObejct = GameObjectsLocator.Instance.Get<Player>()[0];

        normalAttackTimer += Time.deltaTime;
        heavyAttackTimer += Time.deltaTime;

        switch (Behaviors)
        {
            case AttackBehaviors.normal:
                NormalAttack();
                break;
            case AttackBehaviors.heavy:
                HeavyAttack();
                break;
            case AttackBehaviors.rotation:
                break;
            case AttackBehaviors.destroy:
                break;
            case AttackBehaviors.summon:
                break;
            default:
                break;
        }
    }

    void NormalAttack()
    {
        isMoving = Vector3.Distance(targetObejct.transform.position, transform.position) > normalAttackRange;
        golemAnimator.SetBool("Moving", isMoving);
        if (canMove && isMoving)
        {
            Vector3 direction = targetObejct.transform.position - transform.position;
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            golemSprite.flipX = targetObejct.gameObject.transform.position.x >= transform.position.x;
            return;
        }

        if (normalAttackTimer >= normalAttackCooldown)
        {
            golemAnimator.SetTrigger("Attack1");
            normalAttackTimer = 0f;
        }
    }

    void HeavyAttack()
    {
        isMoving = Vector3.Distance(targetObejct.transform.position, transform.position) > heavyAttackRange;
        golemAnimator.SetBool("Moving", isMoving);
        if (canMove && isMoving)
        {
            Vector3 direction = targetObejct.transform.position - transform.position;
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            golemSprite.flipX = targetObejct.gameObject.transform.position.x >= transform.position.x;
            return;
        }

        if (heavyAttackTimer >= heavyAttackCooldown)
        {
            golemAnimator.SetTrigger("Attack2");
            heavyAttackTimer = 0f;

            // Start Effect
            int face = targetObejct.gameObject.transform.position.x >= transform.position.x ? 1 : -1;
            heavyAttackEffectObject.transform.localPosition = new Vector3(heavyAttackEffectOffset * face, 0f, 0f);
            heavyAttackEffectObject.SetActive(true);
        }
    }

    void SetCanMove(int value)
    {
        canMove = value == 1;
    }

}
