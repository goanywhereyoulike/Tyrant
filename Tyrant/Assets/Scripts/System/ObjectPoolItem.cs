using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://www.raywenderlich.com/847-object-pooling-in-unity

[System.Serializable]
public struct ObjectPoolItem
{
    public string tagName;
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;
}
