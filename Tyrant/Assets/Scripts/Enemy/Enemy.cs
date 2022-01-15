using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour, GameObjectsLocator.IGameObjectRegister, IDamageable, IPushable
{
    [SerializeField]
    EnemyState enemyState = new EnemyState();

    [SerializeField]
    private Animator burningAnimator;

    protected Rigidbody2D rb;

    protected SpriteRenderer spriteRenderer;

    protected NodePath nodePath;
    
    protected RaycastHit2D hit;
    //public Transform target;
    //pathfinding
    protected List<NodePath.Node> closedList = new List<NodePath.Node>();
    protected List<NodePath.Node> mPath = new List<NodePath.Node>();
    protected List<GameObject> targets = new List<GameObject>();
    protected List<Vector3> nextNodes = new List<Vector3>();

    public Transform mTarget;
    protected Transform mMainTarget;

    [SerializeField]
    protected float wDetectRange;

    protected float Health;
    protected float damage;
    protected float moveSpeed;
    protected float oMoveSpeed;
    protected float mass;
    protected float waitAttacks;
    protected float attackSpeed;
    protected float stopDistance;
    protected float lastDistance;
    protected float detectRange;
    protected float attacktime;
    protected float distance;
    protected float mainTargetDistance;
    protected float delayTime;
    protected int pathcount;
    protected float armor;

    protected bool isGetBlock = false;
    protected bool isDead = false;
    protected bool findPath = false;
    protected bool findTarget = false;
    protected bool search = false;
    protected bool isSpawn = false;

    protected bool firstFramePath = false;
    protected bool canFind = false;

    private List<Vector2> debug;

    protected List<Player> mainTarget = null;
    Pathfinding path = null;

    protected Animator anim;
    public EnemyState EnemyState { get => enemyState; /*set => enemyState = value;*/ }
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
    public bool IsDead
    {
        get => isDead;
        set
        {
            isDead = value;
            if (isDead)
            {
                //AudioManager.Instance.PlaySFX(6);
                gameObject.SetActive(false);
                isSpawn = false;
                IsSlow = false;
                //UnRegisterToLocator();
            }
        }
    }


    private bool isSlow = false;
    protected bool IsSlow
    {
        get => isSlow;
        set
        {
            isSlow = value;
            spriteRenderer.material.color = isSlow ? new Color(1.0f, 118.0f, 255.0f) : Color.white;
        }
    }

    public bool IsBurning
    {
        get => isBurning;
        set
        {
            isBurning = value;
            spriteRenderer.material.color = isBurning ? new Color(255f, 0f, 0f) : Color.white;
        }
    }

    private bool isBurning = false;
    public bool IsLighting = false;
    public int LightingIndex = -1;
    public int LightingTowerIndex = -1;

    protected virtual void Start()
    {
        ReUse();
        path = new Pathfinding();
        nodePath = new NodePath();
        anim = GetComponent<Animator>();
        mMainTarget = mTarget;
        detectObject();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        RegisterToLocator();
        oMoveSpeed = moveSpeed;
    }

    private float selfBurningDamage = 0;
    private float selfBurningArmoDamage = 0;

    protected virtual void Update()
    {
        //GetComponent<Rigidbody2D>().Sleep();
        if (!isSpawn)
        {
            mainTarget = GameObjectsLocator.Instance.Get<Player>();
            ReUse();
           
            isSpawn = true;
        }

        if (isBurning)
        {
            TakeDamage(selfBurningDamage);
            BurnArmor(selfBurningArmoDamage);
        }

        if (!isGetBlock)
        {
            if (GameObjectsLocator.Instance.Get<Block>() != null)
            {
                var tilemap = GameObjectsLocator.Instance.Get<Block>();
                nodePath.init(tilemap[0].tilemap.cellBounds.size.x, tilemap[0].tilemap.cellBounds.size.y);
                isGetBlock = true;
            }
        }

        //if(isSlow)
        //{
        //    SlowDown(1.5f);
        //}
        //if (!RoomManager.Instance.IsTutorial)
        //{
        //    if (targets != null)
        //        targets.Clear();
        //}

        if (targets != null)
            targets.Clear();

        IsEnemyDead();
        detectObject();
        CheckWall(wDetectRange);

        //behaviours.Update();

        if (IsDead)
        {
            return;
        }
        // check mtarget is null or check deafult target is enemy,but not in range
        if (mTarget == null)
        {
            findTarget = false;
            mMainTarget = mainTarget[0].transform;
        }

        //check is base in the range, then attack base first
        if (mMainTarget != null)
        {
            mainTargetDistance = Vector3.Distance(transform.position, mMainTarget.position);
            if (IsTargetInRange(mainTargetDistance))
            {
                mTarget = mMainTarget;
                findTarget = true;
            }
        }

        //check enemy findtarget
        if (findTarget == false)
        {
            mTarget = mMainTarget;
            FindClosetObject();
            if (!firstFramePath && mTarget != null)
                GetPath();

            float dis = Vector3.Distance(mTarget.position, transform.position);
            float t = 0.1f + dis * 0.1f;
            t = Mathf.Clamp(t, 0.1f, 1f);
            if (firstFramePath && mTarget)
            {
                if (!canFind)
                {
                    canFind = true;
                    StartCoroutine(DelayFindPath(t));
                }
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
                        if (rb)
                        {
                            rb.velocity = Vector3.zero;
                        }
                        pathcount++;
                    }
                }
            }
        }
        else
        {
            if (!firstFramePath && mTarget != null)
                GetPath();

            float dis = Vector3.Distance(mTarget.position, transform.position);
            float t = 0.1f + dis * 0.1f;
            t = Mathf.Clamp(t, 0.1f, 1f);
            if (firstFramePath && mTarget)
            {
                if (!canFind)
                {
                    canFind = true;
                    StartCoroutine(DelayFindPath(t));
                }
            }

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
                            if(rb)
                            {
                                rb.velocity = Vector3.zero;
                            }
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
                        //StartCoroutine("Attack");
                        AttackBehavior();
                        attacktime = Time.time + EnemyState.TimeBetweenAttacks;
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

        spriteRenderer.flipX = transform.position.x > mTarget.position.x;
    }

    protected void CheckWall(float wDetectRange)
    {
        RaycastHit2D upHit = Physics2D.Raycast(transform.position, Vector2.up, wDetectRange);
        if (upHit.collider != null && upHit.collider.tag == "Wall")
        {
            if (rb)
            {
                rb.velocity = Vector3.down;
                rb.angularVelocity = 0.0f;
            }
           Vector2 Position = transform.position;
            Position.y = transform.position.y - 1;
            transform.position = Position;
        }
       Debug.DrawRay(transform.position, Vector2.up, Color.green, wDetectRange);

        RaycastHit2D downHit = Physics2D.Raycast(transform.position, Vector2.down, wDetectRange+1);
        if (downHit.collider != null && downHit.collider.tag == "Wall")
        {
            if (rb)
            {
                rb.velocity = Vector3.up;
                rb.angularVelocity = 0.0f;
            }
            Vector2 Position = transform.position;
            Position.y = transform.position.y + 1;
            transform.position = Position;
        }
        //Debug.DrawRay(transform.position, Vector2.down, Color.green, wDetectRange);
        RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.right, wDetectRange);
        if (rightHit.collider != null && rightHit.collider.tag == "Wall")
        {
            if (rb)
            {
                rb.velocity = Vector3.left;
                rb.angularVelocity = 0.0f;
            }
            Vector2 Position = transform.position;
            Position.x = transform.position.x - 1;
            transform.position = Position;
        }
        //Debug.DrawRay(transform.position, Vector2.left, Color.green, 2);
        RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.left, wDetectRange);
        if (leftHit.collider != null && leftHit.collider.tag == "Wall")
        {
            if (rb)
            {
                rb.velocity = Vector3.right;
                rb.angularVelocity = 0.0f;
            }
            Vector2 Position = transform.position;
            Position.x = transform.position.x + 1;
            transform.position = Position;
        }
        // Debug.DrawRay(transform.position, Vector2.right, Color.green, 2);
    }
    //------------------attck animation------------------------
    //IEnumerator Attack()
    //{
    //    Vector2 originalPosition = transform.position;

    //    Vector2 targetPosition = mTarget.position;

    //    float percent = 0;
    //    while (percent <= 1)
    //    {
    //        percent += Time.deltaTime * EnemyState.AttackSpeed;

    //        float formula = (-Mathf.Pow(percent, 2) + percent) * 4;

    //        transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);

    //        yield return null;
    //    }
    //}

    protected virtual void AttackBehavior()
    {

    }

    public void SlowDown(float slowTime, float newSpeed)
    {
        IsSlow = true;
        float oldSpeed = enemyState.MaxMoveSpeed;
        moveSpeed = newSpeed;
        StartCoroutine(SpeedBack(slowTime, oldSpeed));
    }

    IEnumerator SpeedBack(float slowTime, float oldSpeed)
    {
        yield return new WaitForSeconds(slowTime);
        IsSlow = false;
        moveSpeed = oldSpeed;
    }

    protected IEnumerator DelayFindPath(float delayTime)
    {
        if (canFind)
        {
            yield return new WaitForSeconds(delayTime);
            //Debug.Log("Delay111");
            GetPath();
            canFind = false;
        }
    }

    protected void detectObject()
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
        //foreach (GameObject target in GameObject.FindGameObjectsWithTag("ChainTower"))
        //{
        //    targets.Add(target);
        //}
        //foreach (GameObject target in GameObject.FindGameObjectsWithTag("CannonTower"))
        //{
        //    targets.Add(target);
        //}
    }

    protected void FindClosetObject()
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

    protected bool IsTargetInRange(float distance)
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

    protected virtual void ReUse()
    {
        canFind = false;
        isBurning = false;
        mTarget = null;
        mPath.Clear();
        findPath = false;
        firstFramePath = false;
        isDead = false;
        Health = enemyState.MaxHealth;
        damage = enemyState.MaxDamage;
        moveSpeed = enemyState.MaxMoveSpeed;
        mass = enemyState.Mass;
        waitAttacks = enemyState.TimeBetweenAttacks;
        attackSpeed = enemyState.AttackSpeed;
        stopDistance = enemyState.StopDistance;
        detectRange = enemyState.DetectRange;
        armor = enemyState.MaxArmor;
        burningAnimator.gameObject.SetActive(IsBurning);
    }
    protected void IsEnemyDead()
    {
        if (Health <= 0.0f)
        {
            IsDead = true;
        }
    }

    public void Burn(float burnDamage, float damage, float burnTime)
    {
        if (IsBurning)
            return;
        IsBurning = true;
        burningAnimator.gameObject.SetActive(IsBurning);
        burningAnimator.SetBool("Burning", IsBurning);
        selfBurningDamage = damage;
        selfBurningArmoDamage = burnDamage;
        StartCoroutine(StartBurn(burnDamage, damage, burnTime));
    }

    IEnumerator StartBurn(float burnDamage, float damage, float burnTime)
    {
        yield return new WaitForSeconds(burnTime);
        IsBurning = false;
        burningAnimator.SetBool("Burning", IsBurning);
    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
        //AudioManager.instance.PlaySFX(5);
    }

    public virtual void BurnArmor(float buringDamge)
    {
        armor -= buringDamge;
    }

    protected void GetPath()
    {
        firstFramePath = true;
        if (path.Search((Vector2)transform.position, (Vector2)mTarget.position))
        {
            findPath = true;
            search = true;
            closedList.Clear();
            closedList = path.CloseList;
        }

        if (findPath)
        {
            mPath.Clear();
            nextNodes.Clear();
            pathcount = 2;

            // Beginning from the end node, trace back to it's parent one at a timec
            NodePath.Node path = closedList[closedList.Count - 1];
            while (path != null)
            {
                mPath.Add(path);
                path = path.parent;
            }

            // Once we recorded all the position from end to start, we need to reverse
            // them to get the correct order
            mPath.Reverse();
        }
    }

    public void RegisterToLocator()
    {
        GameObjectsLocator.Instance.Register<Enemy>(this);
    }

    public void UnRegisterToLocator()
    {
        GameObjectsLocator.Instance.Unregister<Enemy>(this);
    }

    public void BePushed(Vector3 force)
    {
        //rb.AddForce(force);
        transform.position += force;
    }

}