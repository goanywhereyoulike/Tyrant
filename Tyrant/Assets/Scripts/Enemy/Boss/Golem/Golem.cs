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

    [Header("Property:")]
    [SerializeField]
    private float health = 0.0f;
    [SerializeField]
    private float moveSpeed = 0.0f;

    [Header("Attack Attribute:")]
    [SerializeField]
    private float normalAttackRange = 0.0f;
    [SerializeField]
    private float normalAttackCooldown = 0.0f;

    public AttackBehaviors Behaviors { get; set; }
    public float Health { get => health; set => health = value; }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    private Player targetObejct = null;
    private SpriteRenderer golemSprite = null;
    private Animator golemAnimator = null;
    private float normalAttackTimer = 0.0f;
    private bool isMoving = false;
    private bool canMove = false;

    private void Start()
    {
        Behaviors = AttackBehaviors.normal;
        normalAttackTimer = normalAttackCooldown;
        golemSprite = GetComponent<SpriteRenderer>();
        golemAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!targetObejct)
            targetObejct = GameObjectsLocator.Instance.Get<Player>()[0];

        golemSprite.flipX = targetObejct.gameObject.transform.position.x >= transform.position.x;

        switch (Behaviors)
        {
            case AttackBehaviors.normal:
                NormalAttack();
                break;
            case AttackBehaviors.heavy:
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
        normalAttackTimer += Time.deltaTime;
        isMoving = Vector3.Distance(targetObejct.transform.position, transform.position) > normalAttackRange;
        golemAnimator.SetBool("Moving", isMoving);
        if (canMove && isMoving)
        {
            Vector3 direction = targetObejct.transform.position - transform.position;
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            return;
        }

        if (normalAttackTimer >= normalAttackCooldown)
        {
            golemAnimator.SetTrigger("Attack1");
            normalAttackTimer = 0f;
        }

    }

    void SetCanMoveToTrue(int value)
    {
        canMove = value == 1;
    }

}
