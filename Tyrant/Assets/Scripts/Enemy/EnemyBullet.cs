using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    public float bulletSpeed;
    int damage;
    float a;
    float b;
    float distance;
    float range;
    float speed;
    Vector2 shootPosition;
    Vector2 targetPosition;
    Vector2 direction;
    public Vector2 Position { get => targetPosition; set => targetPosition = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
    public int Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }

    private void Start()
    {
        shootPosition = transform.position;
        animator = GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        a = shootPosition.x - transform.position.x;
        b = shootPosition.y - transform.position.y;
        distance = Mathf.Sqrt((a * a) + (b * b));
        if ((Vector2)transform.position == Position)
        {
            gameObject.SetActive(false);
        }
        speed = bulletSpeed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, Position, speed);
        Vector3 direction = (Vector3)targetPosition - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Tower" || collider.gameObject.tag == "Base")
        {
            IDamageable Targets = collider.gameObject.GetComponent<IDamageable>();
            if (Targets == null)
            {
                Targets = collider.gameObject.GetComponentInChildren<IDamageable>();
            }
            Targets.TakeDamage(Damage);
            //gameObject.SetActive(false);
            animator.SetBool("End", true);
            Debug.Log("attack");
        }

        if (collider.gameObject.tag == "Wall")
        {
            DisableBullet();
        }
    }

    public void DisableBullet()
    {
        animator.SetBool("End", false);
        gameObject.SetActive(false);
    }
}
