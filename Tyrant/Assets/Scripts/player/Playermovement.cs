using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        isMovingChanged += () => playerAnimation.IsMoving = IsMoving;
    }

    void Update()
    {
        Vector3 moving = new Vector3(InputManager.Instance.GetAxisRaw("Horizontal"), InputManager.Instance.GetAxisRaw("Vertical"), 0.0f) * MoveSpeed * Time.deltaTime;

        if (moving == Vector3.zero)
        {
            IsMoving = false;
        }
        else
        {
            IsMoving = true;
            transform.position += moving;
        }
    }
}
