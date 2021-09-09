using UnityEngine;
using UnityEngine.UI;

public class PSC : MonoBehaviour, GameObjectsLocator.IGameObjectRegister, IDamageable
{
    public Slider healthSilder;
    [SerializeField]
    private EnemyState enemyState;

    public EnemyState EnemyState { get => enemyState; set => enemyState = value; }

    SpriteRenderer spriteRenderer;

    private float health = 0f;

    private bool isDead = false;
    public bool IsDied
    {
        get => isDead;
        set
        {
            if (isDead)
            {
                gameObject.SetActive(false);
                UnRegisterToLocator();
            }
            isDead = value;
        }
    }

    public float Health { get => health; set => health = value; }

    private void Start()
    {
        healthSilder.maxValue = EnemyState.MaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Health = EnemyState.MaxHealth;
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
        Health -= damage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
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
}
