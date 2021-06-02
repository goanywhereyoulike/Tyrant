using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    EnemyState enemyState = new EnemyState();

    //public Transform target;
    private List<GameObject> targets = new List<GameObject>();
    public Transform mTarget;

    private float Health;
    private float Damage;
    private float moveSpeed;
    private float mass;
    private float waitAttacks;
    private float attackSpeed;
    private float stopDistance;
    private float lastDistance;
    private float detectRange;
    
    private float attacktime;
    private float distance;
    bool findTarget = false;
    bool isDead = false;

    StaticMachine behaviours = null;

    public EnemyState EnemyState { get => enemyState; set => enemyState = value; }
    public float MoveSpeed { get => moveSpeed;}
    public bool IsDead { get => isDead;
        set
        { 
            if(isDead)
            {
                gameObject.SetActive(false);
                ReUse();
            }
                isDead = value; }
    }
    /// <summary>

    private Animator anim;
    /// </summary>

    // Start is called before the first frame update
    void Awake()
    {
        ReUse();
        behaviours = gameObject.GetComponent<StaticMachine>();
        behaviours.setEnemy(this);
        behaviours.AllBehaviour();
        anim = GetComponent<Animator>();
        detectObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (targets != null)
        {
            targets.Clear();
        }

        detectObject();
        behaviours.Update();

        if(mTarget == null)
        {
            findTarget = false;
        }

        if (findTarget == false)
        {
            FindClosetObject();
        }
        else
        {
            distance = Vector3.Distance(transform.position, mTarget.position);
            if (IsTargetInRange(distance))
            {
                if (distance > stopDistance)
                {
                    enemyState.force = behaviours.ForceCalculate();
                    enemyState.acceleration = enemyState.force / enemyState.Mass;
                    enemyState.velocity += enemyState.acceleration;

                    //animation
                    anim.SetBool("isRunning", true);

                }
                else
                {
                    enemyState.velocity = Vector3.zero;

                    if (Time.time >= attacktime)
                    {
                        StartCoroutine("Attack");
                        attacktime = Time.time + enemyState.TimeBetweenAttacks;
                    }

                    //animation
                    anim.SetBool("isRunning", false);
                }
            }
            else
            {
                findTarget = false;
            }

            transform.position += enemyState.velocity;

        }
    }
    //------------------attck animation------------------------
    IEnumerator Attack()
    {
        //player.GetComponent<Player>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;

        Vector2 targetPosition = mTarget.position;

        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime * enemyState.AttackSpeed;

            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;

            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);

            yield return null;
        }
    }

    void detectObject()
    {
        if (GameObject.FindGameObjectsWithTag("Player") != null || GameObject.FindGameObjectsWithTag("Tower") != null)
        {
            foreach (GameObject target in GameObject.FindGameObjectsWithTag("Player"))
            {
                targets.Add(target);
            }
            foreach (GameObject target in GameObject.FindGameObjectsWithTag("Tower"))
            {
                targets.Add(target);
            }
        }
    }

    void FindClosetObject()
    {
        lastDistance = 0;
        for (int i = 0; i < targets.Count; ++i)
        {
            distance = Vector3.Distance(transform.position, targets[i].transform.position);
            if (lastDistance == 0)
            {
                lastDistance = distance;
            }

            if (distance < lastDistance)
            {
                lastDistance = distance;
                if (IsTargetInRange(lastDistance))
                {
                    mTarget = targets[i].transform;
                    findTarget = true;
                }
            }
            else
            {
                if (IsTargetInRange(lastDistance))
                {
                    mTarget = targets[0].transform;
                    findTarget = true;
                }
            }
        }
    }

    bool IsTargetInRange(float distance)
    {
        if (distance >= enemyState.DetectRange)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health <= 0.0f)
        {
            IsDead = true;
            
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawWireSphere(transform.position, enemyState.DetectRange);
        // Gizmos.DrawSphere(transform.position, enemyState.DetectRange);
    }
 
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == mTarget.name)
        {
            if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Tower")
            {
                IDamageable Targets = collider.gameObject.GetComponent<IDamageable>();
                Targets.TakeDamage(Damage);
            }
        }
        Debug.Log("attack");
    }

    void ReUse()
    {
        Health = enemyState.MaxHealth;
        Damage = enemyState.MaxDamage;
        moveSpeed = enemyState.MaxMoveSpeed;
        mass = enemyState.Mass;
        waitAttacks = enemyState.TimeBetweenAttacks;
        attackSpeed = enemyState.AttackSpeed;
        stopDistance = enemyState.StopDistance;
        detectRange = enemyState.DetectRange;
    }
}