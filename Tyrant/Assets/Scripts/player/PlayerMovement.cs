using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed { get; set; }

    public System.Action isMovingChanged = null;
    public bool IsMoving
    {
        get => isMoving;
        set
        {
            isMoving = value;
            isMovingChanged?.Invoke();
        }
    }

    private bool isMoving = false;

    private PlayerAnimation playerAnimation;

    private Rigidbody2D rb;

    private Vector3 moveDirection;

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        rb = GetComponent<Rigidbody2D>();
        isMovingChanged += () => playerAnimation.IsMoving = IsMoving;
    }

    private void Update()
    {
        rb.Sleep();
        moveDirection = Vector3.zero;
        if (InputManager.Instance.GetKey("GoUp"))
            moveDirection.y = 1;
        if (InputManager.Instance.GetKey("GoDown"))
            moveDirection.y = -1;
        if (InputManager.Instance.GetKey("GoRight"))
            moveDirection.x = 1;
        if (InputManager.Instance.GetKey("GoLeft"))
            moveDirection.x = -1;
    }

    void FixedUpdate()
    {
        Vector3 moving = new Vector3(moveDirection.x, moveDirection.y) * MoveSpeed * Time.deltaTime;
        Vector3 newPos = transform.position + moving;

        if (moving == Vector3.zero)
        {
            IsMoving = false;
        }
        else
        {
            IsMoving = true;
            rb.MovePosition(newPos);
        }
    }
}
