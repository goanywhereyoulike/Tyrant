using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Items> itemList;

    public Inventory()
    {
        itemList = new List<Items>();
        Additem(new Items { itemType = Items.ItemType.gun, amount = 1 });
        Additem(new Items { itemType = Items.ItemType.gold, amount = 1 });
        Additem(new Items { itemType = Items.ItemType.healthpack, amount = 1 });
        Debug.Log(itemList.Count);
    }

    public void Additem(Items items)
    {
        itemList.Add(items);
    }

    public List<Items> GetItemsList()
    {
        return itemList;
    }
}
