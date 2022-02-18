using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using BehaviorDesigner.Runtime.Tasks;

public class PSC : MonoBehaviour, GameObjectsLocator.IGameObjectRegister, IDamageable, IPushable
{
    public Slider healthSilder;
    [SerializeField]
    private EnemyState enemyState;
    [SerializeField]
    private Transform firePoint;

    public EnemyState EnemyState { get => enemyState; set => enemyState = value; }

    SpriteRenderer spriteRenderer;

    private float health = 0f;

    private bool isDead = false;

    public System.Action shootAnimFinished;
    public System.Action attackAnimFinished;
    public bool IsDead
    {
        get => isDead;
        set
        {
            isDead = value;
            if (isDead)
            {
                gameObject.SetActive(false);
                UnRegisterToLocator();
                SceneManager.LoadScene("WinScene");
            }
        }
    }

    public float Health { get => health; set => health = value; }
    public Animator Animator { get => animator; private set => animator = value; }
    public Transform FirePoint { get => firePoint; private set => firePoint = value; }

    private Animator animator;
    private void Start()
    {
        ObjectPoolManager.Instance.InstantiateObjects("normalenemy");
        ObjectPoolManager.Instance.InstantiateObjects("rangeEnemy");
        ObjectPoolManager.Instance.InstantiateObjects("Level1Boss");
        ObjectPoolManager.Instance.InstantiateObjects("bombenemy");
        ObjectPoolManager.Instance.InstantiateObjects("armorenemy");
        ObjectPoolManager.Instance.InstantiateObjects("DropItem");
        ObjectPoolManager.Instance.InstantiateObjects("enemyBullet");
        healthSilder.maxValue = EnemyState.MaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        Health = EnemyState.MaxHealth;
        RegisterToLocator();
    }
    private void Update()
    {
        healthSilder.value = Health;
    }

    public void RegisterToLocator()
    {
        GameObjectsLocator.Instance.Register<PSC>(this);
    }

    public void UnRegisterToLocator()
    {
        GameObjectsLocator.Instance.Unregister<PSC>(this);
    }

    public void TakeDamage(float damage)
    {
        if (Health>0)
        {
            Health -= damage;
        }
        else
        {
            IsDead = true;
        }

    }

    public void BePushed(Vector3 force)
    {
        //rb.AddForce(force);
        transform.position += force;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Tower" || collision.gameObject.tag == "Base")
        {
            IDamageable Targets = collision.gameObject.GetComponent<IDamageable>();
            if (Targets == null)
            {
                Targets = collision.gameObject.GetComponentInChildren<IDamageable>();
            }
            Targets.TakeDamage(EnemyState.MaxDamage);
            Debug.Log("attack");
        }
    }

    public void onAnimationFinished()
    {
        shootAnimFinished?.Invoke();
    }

    public void onAttackFinished()
    {
        attackAnimFinished?.Invoke();
    }
}
