using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    void Update()
    {
        if(InputManager.Instance.GetKey("GoUp"))
        {
           // Debug.Log("Goup");
        }

        if (InputManager.Instance.GetKeyDown("GoDown"))
        {
            Debug.Log("GoDown");
        }

        float holdingTime;
        if (InputManager.Instance.GetKey("GoUp", out holdingTime) && holdingTime >= 5)
        {
           // Debug.Log("KeyHold works");
        }
        if (holdingTime < 5)
        {
           // Debug.Log(holdingTime);
        }

        if (InputManager.Instance.GetKeyDown("OpenInventory"))
        {
            Debug.Log("Open Inventory");
        }

        if (InputManager.Instance.GetKeyDown("Shoot"))
        {
            Debug.Log("Shoot");
        }

        Debug.Log(InputManager.Instance.GetAxisRaw("Horizontal"));
        //Debug.Log(Input.GetAxisRaw("Horizontal"));

        //Debug.Log(InputManager.Instance.GetAxis("Horizontal"));
        //Debug.Log(Input.GetAxis("Horizontal"));
    }
}
