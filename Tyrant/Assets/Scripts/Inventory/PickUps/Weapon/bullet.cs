using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed = 4.0f;
    public int damage = 50;
    Vector3 direction= Vector3.zero;

    public Vector3 Direction { get { return direction; } set { direction = value; } }

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
   
