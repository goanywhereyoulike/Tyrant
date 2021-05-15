using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Reference: https://www.raywenderlich.com/847-object-pooling-in-unity

[System.Serializable]
public class ObjectPoolItem
{
    public GameObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;
}
