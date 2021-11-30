using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFix : MonoBehaviour
{
    private void Awake()
    {
        transform.rotation = Quaternion.identity;
    }
}
