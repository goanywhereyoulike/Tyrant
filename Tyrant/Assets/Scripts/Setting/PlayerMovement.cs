using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject BulletPrefab;

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;
        if (InputManager.Instance.GetKey("GoRight"))
        {
            
            position.x += 0.01f;
            transform.position = position;
        
        }
        if (InputManager.Instance.GetKey("GoLeft"))
        {
            position.x -= 0.01f;
            transform.position = position;

        }
        if (InputManager.Instance.GetKey("GoUp"))
        {
            position.y += 0.01f;
            transform.position = position;

        }
        if (InputManager.Instance.GetKey("GoDown"))
        {
            position.y -= 0.01f;
            transform.position = position;

        }
    }
}
