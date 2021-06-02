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
        Vector2 position = transform.position;
        if (InputManager.Instance.GetKey("GoRight"))
        {
            MoveAnimator.SetTrigger("MoveRight");
            position.x += 0.01f;
            transform.position = position;
        
        }
        if (InputManager.Instance.GetKey("GoLeft"))
        {
            MoveAnimator.SetTrigger("MoveLeft");

            position.x -= 0.01f;
            transform.position = position;

        }
        if (InputManager.Instance.GetKey("GoUp"))
        {
            MoveAnimator.SetTrigger("MoveUp");

            position.y += 0.01f;
            transform.position = position;

        }
        if (InputManager.Instance.GetKey("GoDown"))
        {
            MoveAnimator.SetTrigger("MoveDown");

            position.y -= 0.01f;
            transform.position = position;

        }
    }
}
