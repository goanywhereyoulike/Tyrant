using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 4.0f;
    public int damage = 50;
    public Vector3 direction;

    private void Update()
    {
        transform.position += direction * speed *Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            gameObject.SetActive(false);
        }

    }
}
   
