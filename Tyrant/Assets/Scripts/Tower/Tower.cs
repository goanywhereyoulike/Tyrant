using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour, IDamageable
{
    // Start is called before the first frame update

    public GameObject DestroyedTower;

    [SerializeField]
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
        }
        get
        {
            return health;

        }
    }

    [SerializeField]
    private Slider Healthbar;

    PlayerMovement player;
    public Animator animator;
    //public Transform targetpos;
    private Vector3 direction;
    float angle;
    void Awake()
    {
        player = FindObjectOfType<PlayerMovement>();
        animator = GetComponent<Animator>();
        health = 100;
        Healthbar.maxValue = health;
        Healthbar.value = health;
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
        Destroy(gameObject.transform.parent.gameObject);
        Instantiate(DestroyedTower, transform.position, transform.rotation);
    
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        Healthbar.value = Health;
        Debug.Log("Tower"+health);
    }

    private void OnMouseEnter()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.gray;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

}
