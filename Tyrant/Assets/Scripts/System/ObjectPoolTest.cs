using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTest : MonoBehaviour
{
    private void Start()
    {
        ObjectPoolManager.Instance.InstantiateObjects("Bullet");
    }

    GameObject gameObject2B;

    void Update()
    {
        if (InputManager.Instance.GetKeyDown("Shoot"))
        {
            gameObject2B = ObjectPoolManager.Instance.GetPooledObject("Bullet");
            gameObject2B.SetActive(true);
        }
    }
}
