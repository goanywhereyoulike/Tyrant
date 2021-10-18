using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrap_Arrow : MonoBehaviour
{
    // Update is called once per frame

    public float speed = 2.0f;
    SpriteRenderer sprite;
    public float damage = 1.0f;

    private int direction = -1;
    private void Start()
    {

        // Setdirection(sprite);

    }

    public void SetDirection(int dir)
    {
        direction = dir;
    }

    void MoveDirection()
    {
        if (direction == 1)
        {
            transform.position += Vector3.down * Time.deltaTime * speed;
        }
        else if (direction == 2)
        {
            transform.position += Vector3.up * Time.deltaTime * speed;

        }
        else if (direction == 3)
        {
            transform.position += Vector3.right * Time.deltaTime * speed;

        }
        else if (direction == 4)
        {
            transform.position += Vector3.left * Time.deltaTime * speed;

        }



    }


    void Update()
    {
        MoveDirection();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        string tag = collision.gameObject.tag;
        if (player)
        {

            player.TakeDamage(damage);
        
        }
        if (tag == "Player" || tag == "Wall")
        {
            gameObject.SetActive(false);
        }


    }



}
