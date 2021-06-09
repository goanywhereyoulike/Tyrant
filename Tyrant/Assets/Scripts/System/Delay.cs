using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delay : MonoBehaviour
{
    void Update()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(deactive());
        }
    }
    IEnumerator deactive()
    {
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
}
