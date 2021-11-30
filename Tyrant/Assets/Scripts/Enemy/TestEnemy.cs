using UnityEngine;

public class TestEnemy : Enemy
{
    [SerializeField]
    private EnemyUI enemyUi = null;

    [SerializeField]
    private bool isChase = false;

    [SerializeField]
    private bool isTestTarget = false;

    [SerializeField]
    private bool canDead;

    private bool armorEnemy = false;

    Vector3 lastPosition;

    StaticMachine behaviours = null;

    private float currentHelath;

    public bool IsChase { get => isChase; set => isChase = value; }
    public bool IsTestTarget { get => isTestTarget; set => isTestTarget = value; }

    // Start is called before the first frame update

    //private void Awake()
    //{
    //    lastPosition = transform.position;

    //}
    protected override void Start()
    {
        GameObjectsLocator.Instance.Register<Enemy>(this);
        base.Start();
        //if (isChase)
        //{
        //    //behaviours = gameObject.GetComponent<StaticMachine>();
        //    //behaviours.setEnemy(this);
        //    //behaviours.AllBehaviour();
        //}
        spriteRenderer = GetComponent<SpriteRenderer>();
        enemyUi.MaxHealthChanged(EnemyState.MaxHealth);
        enemyUi.HealthChanged(EnemyState.MaxHealth);
        currentHelath = EnemyState.MaxHealth;
        lastPosition = transform.position;
        if (armorEnemy)
        {
            enemyUi.MaxArmorChanged(EnemyState.MaxArmor);
            enemyUi.ArmorChanged(EnemyState.MaxArmor);
        }
        else
        {
            enemyUi.ShutdownArmorBar();
        }
    }

    override public void TakeDamage(float damage)
    {
        currentHelath -= damage;
        if (!canDead)
        { 
            if (currentHelath <= 0.0f)
            {
                currentHelath = EnemyState.MaxHealth;
            } 
        }
        else
        {
            if (currentHelath <= 0.0f)
            {
                currentHelath = EnemyState.MaxHealth;
                gameObject.SetActive(false);
            }
        }
        enemyUi.HealthChanged(currentHelath);
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

    public void Reuse()
    {
        transform.position = lastPosition;
    }

    // Update is called once per frame
    protected override void Update()
    {
        //if (!isSpawn)
        //{
        //    RegisterToLocator();
        //    isSpawn = true;
        //}

        if (IsChase)
        {
            //mainTarget = GameObjectsLocator.Instance.Get<Player>();
            //mTarget = mainTarget[0].transform;
            //behaviours.Update();
            //EnemyState.force = behaviours.ForceCalculate();
            //EnemyState.acceleration = EnemyState.force / EnemyState.Mass;
            //EnemyState.velocity += EnemyState.acceleration;
            //transform.position += EnemyState.velocity;
            base.Update();
        }

        if (IsTestTarget)
        {
            Reuse();
        }
    }
}