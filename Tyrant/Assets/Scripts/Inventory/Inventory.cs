using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    private Dictionary<PickUp, int> inventoryDict = new Dictionary<PickUp, int>();
    public Dictionary<PickUp, int> InventoryDict
    { 
        get => inventoryDict;
        private set
        {
            if (inventoryDict.Count > maxInventoryAmount)
                isFull = true;
            else
            {
                isFull = false;
                inventoryDict = value;
            }
        }
    }

    public int maxPickUpAmount = 0;
    public int maxInventoryAmount = 0;
    private bool isFull = false;

    public bool AddPickUp(PickUp pickUp)
    {
        if (!isFull)
        {
            if (!InventoryDict.ContainsKey(pickUp))
            {
                InventoryDict.Add(pickUp, 1);
                return true;
            }
            else
            {
                if (InventoryDict[pickUp] > maxPickUpAmount)
                    return false;
                else
                {
                    InventoryDict[pickUp]++;
                    return true;
                }
            }
        }
        return false;
    }
    public void DropPickUp(Vector3 dropPosition, PickUp pickUp)
    {
        GameObject dropPickUp = ObjectPoolManager.Instance.GetPooledObject(pickUp.PickUpInfo.PickUpName);
        if (dropPickUp)
        {
            dropPickUp.transform.position = dropPosition;
            dropPickUp.SetActive(true);
            DeletePickUp(pickUp);
        }
    }
    public PickUp GetPickUp(string pickUpName)
    {
        foreach (var pickUp in InventoryDict)
        {
            if (pickUp.Key.PickUpInfo.PickUpName.Equals(pickUpName))
            {
                return pickUp.Key;
            }
        }
        return null;
    }

    public void DeletePickUp(PickUp pickUp)
    {
        if (InventoryDict.ContainsKey(pickUp))
        {
            InventoryDict[pickUp]--;
            if(InventoryDict[pickUp] <= 0)
            {
                InventoryDict.Remove(pickUp);
            }
        }
    }
}
