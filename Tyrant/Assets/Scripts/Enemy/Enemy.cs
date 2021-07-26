using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    EnemyState enemyState = new EnemyState();
    NodePath nodePath;
    //public Transform target;
    private List<NodePath.Node> closedList = new List<NodePath.Node>();
    private List<NodePath.Node> mPath = new List<NodePath.Node>();
    private List<GameObject> targets = new List<GameObject>();
    private List<Vector3> nextNodes = new List<Vector3>();

    public Transform mTarget;
    public Vector3 nextNode;
    Transform mMainTarget;

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
    private float mainTargetDistance;
    private int pathcount =0;
    bool findTarget = false;
    bool isDead = false;
    bool findPath = false;
    bool isGetBlock = false;
    bool search = false;
    StaticMachine behaviours = null;
    Pathfinding path = null;

    public EnemyState EnemyState { get => enemyState; set => enemyState = value; }
    public float MoveSpeed { get => moveSpeed; }
    public bool IsDead
    {
        get => isDead;
        set
        {
            if (isDead)
            {
                gameObject.SetActive(false);
                ReUse();
            }
            isDead = value;
        }
    }
    /// <summary>

    private Animator anim;
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {
        ReUse();
        behaviours = gameObject.GetComponent<StaticMachine>();
        path = new Pathfinding();
        nodePath = new NodePath();
        behaviours.setEnemy(this);
        behaviours.AllBehaviour();
        anim = GetComponent<Animator>();
        mMainTarget = mTarget;
        detectObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGetBlock)
        {
            var tilemap = GameObjectsLocator.Instance.Get<Block>();
            nodePath.init(tilemap[0].tilemap.cellBounds.size.x, tilemap[0].tilemap.cellBounds.size.y);
            isGetBlock = true;
        }

        if (targets != null)
            targets.Clear();

        IsEnemyDead();
        detectObject();
        behaviours.Update();

        // check mtarget is null or check deafult target is enemy,but not in range
        if (mTarget == null)
            findTarget = false;

        //check is base in the range, then attack base first
        mainTargetDistance = Vector3.Distance(transform.position, mMainTarget.position);
        if (IsTargetInRange(mainTargetDistance))
        {
            mTarget = mMainTarget;
            findTarget = true;
        }


        //check enemy findtarget
        if (findTarget == false)
        {
            mTarget = mMainTarget;
            FindClosetObject();
            if (search == false)
            {
                GetPath();
            }

            if (findPath)
            {
                if (pathcount < mPath.Count)
                {
                    Vector2 position = new Vector2(mPath[pathcount].r, mPath[pathcount].c);
                    float speed = MoveSpeed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, position, speed);
                    if ((Vector2)transform.position == position)
                    {
                        pathcount++;
                    }
                }
            }
            //if (findPath)
            //{
            //    Vector2 position = new Vector2(mPath[pathcount].r, mPath[pathcount].c);
            //    if ((Vector2)transform.position == position)
            //    {
            //        float speed = MoveSpeed * Time.deltaTime;
            //        transform.position = Vector2.MoveTowards(transform.position, position, speed);
            //        pathcount++;
            //    }
            //    //enemyState.force = behaviours.ForceCalculate();
            //    //enemyState.acceleration = enemyState.force / enemyState.Mass;
            //    //enemyState.velocity += enemyState.acceleration;
            //}
        }
        else
        {
            if (search == true)
            {
                GetPath();
            }
            //CheckPath();
            ///CheckPath(mPath[pathcount].r, mPath[pathcount].c, MoveSpeed);
            distance = Vector3.Distance(transform.position, mTarget.position);
            if (IsTargetInRange(distance))
            {
                if (distance > stopDistance)
                {
                    //enemyState.force = behaviours.ForceCalculate();
                    //enemyState.acceleration = enemyState.force / enemyState.Mass;
                    //enemyState.velocity += enemyState.acceleration;
                    if (pathcount < mPath.Count)
                    {
                        Vector2 position = new Vector2(mPath[pathcount].r, mPath[pathcount].c);
                        float speed = MoveSpeed * Time.deltaTime;
                        transform.position = Vector2.MoveTowards(transform.position, position, speed);
                        if ((Vector2)transform.position == position)
                        {
                            pathcount++;
                        }
                    }
                    //animation
                    anim.SetBool("isRunning", true);

                }
                else
                {
                   // enemyState.velocity = Vector3.zero;

                    if (Time.time >= attacktime)
                    {
                        StartCoroutine("Attack");
                        attacktime = Time.time + enemyState.TimeBetweenAttacks;
                    }
                    //animation
                    anim.SetBool("isRunning", false);
                }
                search = false;
            }
            else
            {
                findTarget = false;
            }
        }
       // transform.position += enemyState.velocity;
        for (int i = 0; i + 1 < mPath.Count; ++i)
        {
            var from = new Vector3(mPath[i].r, mPath[i].c);
            var to = new Vector3(mPath[i + 1].r, mPath[i + 1].c);
            Debug.DrawLine(from, to, Color.green);
        }
    }
    //------------------attck animation------------------------
    IEnumerator Attack()
    {
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
        foreach (GameObject target in GameObject.FindGameObjectsWithTag("Player"))
        {
            targets.Add(target);
        }
        foreach (GameObject target in GameObject.FindGameObjectsWithTag("Tower"))
        {
            targets.Add(target);
        }
        foreach (GameObject target in GameObject.FindGameObjectsWithTag("Base"))
        {
            targets.Add(target);
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
            //find close target
            if (distance <= lastDistance)
            {
                lastDistance = distance;
                if (IsTargetInRange(lastDistance))
                {
                    mTarget = targets[i].transform;
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
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.name == mTarget.name)
        {
            if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Tower" || collider.gameObject.tag == "Base")
            {
                IDamageable Targets = collider.gameObject.GetComponent<IDamageable>();
                if (Targets == null)
                {
                    Targets = collider.gameObject.GetComponentInChildren<IDamageable>();
                }
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

    void IsEnemyDead()
    {
        if (Health <= 0.0f)
        {
            IsDead = true;
        }
    }

    void GetPath()
    {
        if (path.Search((Vector2)transform.position, (Vector2)mTarget.position))
        {
            findPath = true;
            search = true;
            closedList.Clear();
            closedList = path.CloseList;
            pathcount = 0;
        }

        if (findPath)
        {
            mPath.Clear();
            nextNodes.Clear();
            // Beginning from the end node, trace back to it's parent one at a time
            for (int i =0; i< closedList.Count;i++)
            {
                if(closedList[i].r == (int)mTarget.position.x && closedList[i].c == (int)mTarget.position.y)
                {
                    NodePath.Node path = closedList[i];
                    while (path != null)
                    {
                        mPath.Add(path);
                        path = path.parent;
                    }
                    break;
                }
            }
            // Once we recorded all the position from end to start, we need to reverse
            // them to get the correct order
            mPath.Reverse();
        }
        
        foreach (var node in mPath)
        {
            Vector3 position = new Vector3(node.r, node.c, 0);
            nextNodes.Add(position);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawWireSphere(transform.position, enemyState.DetectRange);
        // Gizmos.DrawSphere(transform.position, enemyState.DetectRange);
    }
}