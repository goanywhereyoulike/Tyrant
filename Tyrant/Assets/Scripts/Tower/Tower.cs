using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject DestroyedTower;

    [SerializeField]
    private int health;

    public int Health
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

    void TowerToTarget(Transform targetpos)
    {

        direction = (targetpos.position - transform.position).normalized;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //Left
        if ((angle < -160.0f && angle >= -180.0f) || (angle > 160.0f && angle <= 180.0f))
        {
            animator.SetFloat("Direction", 0.05f);
        }
        //Left Up
        if (angle <= 160.0f && angle >= 100.0f)
        {
            animator.SetFloat("Direction", 0.2f);

        }
        //Up
        if (angle < 100.0f && angle > 80.0f)
        {
            animator.SetFloat("Direction", 0.3f);

        }
        //Right Up
        if (angle <= 80.0f && angle >= 10.0f)
        {
            animator.SetFloat("Direction", 0.5f);
        }
        //Right
        if (angle < 10.0f && angle > -10.0f)
        {
            animator.SetFloat("Direction", 0.6f);
        }
        //Right Down
        if (angle <= -10.0f && angle >= -80.0f)
        {
            animator.SetFloat("Direction", 0.72f);
        }
        //Down
        if (angle < -80.0f && angle > -100.0f)
        {
            animator.SetFloat("Direction", 0.9f);

        }
        //Left Down
        if (angle <= -100.0f && angle >= -160.0f)
        {
            animator.SetFloat("Direction", 1.0f);

        }


    }

    // Update is called once per frame
    void Update()
    {
        TowerToTarget(player.transform);
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

}
