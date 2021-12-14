using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour, IDamageable, GameObjectsLocator.IGameObjectRegister
{
    public enum TowerType { Basic, Cannon, Chain, Lighting, Taunt };

    public TowerType towertype;
    public TowerTemplate towerData;
    public GameObject DestroyedTower;
    private float health;

    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Die();
            }

            if (health >= towerData.health)
            {
                health = towerData.health;
            }
        }
        get
        {
            return health;

        }
    }

    [SerializeField]
    private Slider Healthbar;

    TowerManager towerMngr;

    PlayerMovement player;
    public Animator animator;
    //public Transform targetpos;
    private Vector3 direction;
    float angle;
    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        health = towerData.health;
        Healthbar.maxValue = health;
        Healthbar.value = health;

        towerMngr = FindObjectOfType<TowerManager>();
        RegisterToLocator();
    }

    // Update is called once per frame
    void Update()
    {
        //TowerToTarget(player.transform);
    }

    public void UpdateHealthBar(float health)
    {
        Healthbar.value = health;

    }


    void Die()
    {
        if (towertype == TowerType.Chain)
        {
            ChainTowerBullet[] bullets =GetComponentsInChildren<ChainTowerBullet>(true);

            foreach (var bullet in bullets)
            {
                if (bullet)
                {
                    bullet.gameObject.transform.parent.transform.parent = null;
                    bullet.gameObject.transform.parent.gameObject.SetActive(false);
                }
            }
        }

        Destroy(gameObject.transform.parent.gameObject);
        Instantiate(DestroyedTower, transform.position, transform.rotation);
        if (towerMngr)
        {
            towerMngr.DecreaseTowerlimit();
        }
        if (towertype == TowerType.Lighting)
        {
            LightingTowerShoot ls = GetComponent<LightingTowerShoot>();
            if (ls)
            {
                List<Enemy> enemies = GameObjectsLocator.Instance.Get<Enemy>();
                List<PSC> levelbosses = GameObjectsLocator.Instance.Get<PSC>();
                if (enemies != null)
                {
                    foreach (var enemy in enemies)
                    {
                        LightingTowerBullet[] bullets = enemy.GetComponentsInChildren<LightingTowerBullet>();
                        foreach (var bullet in bullets)
                        {
                            if (bullet)
                            {
                                bullet.gameObject.transform.parent = null;
                                bullet.gameObject.SetActive(false);
                            }
                        }
                    }
                }

                if (levelbosses != null)
                {
                    foreach (var boss in levelbosses)
                    {
                        LightingTowerBullet[] bullets = boss.GetComponentsInChildren<LightingTowerBullet>();

                        foreach (var bullet in bullets)
                        {
                            if (bullet)
                            {
                                bullet.gameObject.transform.parent = null;
                                bullet.gameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }

    public void TakeDamage(float damage)
    {
        TauntTowerEffect tt = GetComponent<TauntTowerEffect>();
        if (tt)
        {
            if (!tt.IsCoolDown)
            {
                return;

            }
        }
        if (Health > 0)
        {
            Health -= damage;
            Healthbar.value = Health;
        }
    }

    private void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void RegisterToLocator()
    {
        GameObjectsLocator.Instance.Register<Tower>(this);
    }

    public void UnRegisterToLocator()
    {
        GameObjectsLocator.Instance.Unregister<Tower>(this);
    }
}
