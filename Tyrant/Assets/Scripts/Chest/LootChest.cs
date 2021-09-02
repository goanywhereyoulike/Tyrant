using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootChest : Chest
{
    [SerializeField]
    private GameObject[] items;
    [SerializeField]
    private Transform[] dropPositions;
    [SerializeField]
    private int numbersToDrop;
    protected override void OpenChest()
    {
        for(int i=0;i<numbersToDrop;++i)
        {
            GameObject item = Instantiate(items[Random.Range(0, items.Length)], dropPositions[i].position,dropPositions[i].rotation);
        }
    }
}
