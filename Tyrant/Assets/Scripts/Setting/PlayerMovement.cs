using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Animator MoveAnimator;
    // Start is called before the first frame update
    private void Awake()
    {
        MoveAnimator = GetComponent<Animator>();
    }
  
    // Update is called once per frame
    void Update()
    {
        Vector2 mPosition = transform.position;
        if (InputManager.Instance.GetKey("GoRight"))
        {

            MoveAnimator.SetTrigger("MoveRight");
            mPosition.x += 0.01f;
            transform.position = mPosition;
        
        }
        if (InputManager.Instance.GetKey("GoLeft"))
        {
            MoveAnimator.SetTrigger("MoveLeft");
            mPosition.x -= 0.01f;
            transform.position = mPosition;

        }
        if (InputManager.Instance.GetKey("GoUp"))
        {
            MoveAnimator.SetTrigger("MoveUp");
            mPosition.y += 0.01f;
            transform.position = mPosition;

        }
        if (InputManager.Instance.GetKey("GoDown"))
        {
            MoveAnimator.SetTrigger("MoveDown");
            mPosition.y -= 0.01f;
            transform.position = mPosition;

        }
    }
}
