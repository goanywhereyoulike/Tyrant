using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerFacing
    {
        Up,
        Down,
        Left,
        Right
    }
    public PlayerFacing playerFacing;
    Animator MoveAnimator;
    // Start is called before the first frame update
    private void Awake()
    {
        MoveAnimator = GetComponent<Animator>();
    }
  
    // Update is called once per frame
    void Update()
    {
        playerFacing = PlayerFacing.Down;
        Vector2 mPosition = transform.position;
        if (InputManager.Instance.GetKey("GoRight"))
        {
            playerFacing = PlayerFacing.Right;
            MoveAnimator.SetTrigger("MoveRight");
            mPosition.x += 0.01f;
            transform.position = mPosition;
        
        }
        if (InputManager.Instance.GetKey("GoLeft"))
        {
            playerFacing = PlayerFacing.Left;
            MoveAnimator.SetTrigger("MoveLeft");
            mPosition.x -= 0.01f;
            transform.position = mPosition;

        }
        if (InputManager.Instance.GetKey("GoUp"))
        {
            playerFacing = PlayerFacing.Up;
            MoveAnimator.SetTrigger("MoveUp");
            mPosition.y += 0.01f;
            transform.position = mPosition;

        }
        if (InputManager.Instance.GetKey("GoDown"))
        {
            playerFacing = PlayerFacing.Down;
            MoveAnimator.SetTrigger("MoveDown");
            mPosition.y -= 0.01f;
            transform.position = mPosition;

        }
    }
}
