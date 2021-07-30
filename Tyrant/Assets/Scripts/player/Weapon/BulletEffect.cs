using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    public void OnEffectCompleted()
    {
        gameObject.SetActive(false);
    }
}
