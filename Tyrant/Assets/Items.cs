using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum ItemType
    {
        gun,
        gold,
        healthpack,
    }

    public ItemType itemType;
    public int amount;
}
