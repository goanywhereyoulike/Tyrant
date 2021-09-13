using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PSC : MonoBehaviour, GameObjectsLocator.IGameObjectRegister, IDamageable, IPushable
{
    public Slider healthSilder;
    [SerializeField]
    private EnemyState enemyState;

    public EnemyState EnemyState { get => enemyState; set => enemyState = value; }

    SpriteRenderer spriteRenderer;

    private float health = 0f;

    private bool isDead = false;
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
                SceneManager.LoadScene("Win");
            }
        }
    }

    public float Health { get => health; set => health = value; }

    private void Start()
    {
        healthSilder.maxValue = EnemyState.MaxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
}
