using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    private Inventory inventory;
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 movement;
   
    private void Awake()
    {
        inventory = new Inventory();
        
    }
    void Update()
    {
        movement.x= Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed*Time.fixedDeltaTime);
    }
}
