using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputTest : MonoBehaviour
{
    void Update()
    {
        if(InputManager.Instance.GetKey("GoUp"))
        {
            Debug.Log("Goup");
        }

        if (InputManager.Instance.GetKeyDown("GoDown"))
        {
            Debug.Log("GoDown");
        }

        if (InputManager.Instance.GetKeyDown("GoLeft"))
        {
            Debug.Log("GoLeft");
        }

        if (InputManager.Instance.GetKeyUp("GoRight"))
        {
            Debug.Log("GoRight");
        }
    }
}
