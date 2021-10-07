using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerange : MonoBehaviour
{
    public float bulletSpeed;
    int damage;
    float a;
    float b;
    float distance;
    float range;
    float speed;
    bool attacktarget=false;
    bool back = false;
    Vector2 shootPosition;
    Vector2 targetPosition;
    Vector2 direction;

    public Vector2 ShootPosition { get => shootPosition; set => shootPosition = value; }
    public Vector2 Position { get => targetPosition; set => targetPosition = value; }
    public Vector3 Direction { get => direction; set => direction = value; }
    public int Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }

    void Update()
    {
        speed = bulletSpeed * Time.deltaTime;
        if ((Vector2)transform.position == Position)
        {
            back = true;
        }

        if ((Vector2)transform.position == shootPosition)
        {
            if (back)
            {
                gameObject.SetActive(false);
                Initialized();
            }
        }

        if (back)
        {
            transform.position = Vector2.MoveTowards(transform.position, shootPosition, speed);
        }

        if (attacktarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, Position, -speed);
        }
        else
        {
            if (!back)
            {
                transform.position = Vector2.MoveTowards(transform.position, Position, speed);
            }
        }
    }


    private void Initialized()
    {
        attacktarget = false;
        back = false;
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
            attacktarget = true;
            Debug.Log("attack");
        }

        if (collider.gameObject.tag == "Wall")
        {
            gameObject.SetActive(false);
            Initialized();
        }

        if (collider.gameObject.tag == "Enemy")
        {
            if (attacktarget)
            {
                gameObject.SetActive(false);
                Initialized();
            }
        }
    }
}
