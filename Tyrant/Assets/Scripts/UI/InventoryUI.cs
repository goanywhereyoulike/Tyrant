using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    Inventory inventory;
    private InventorySlots[] slots;
    public GameObject slotContainer;

    // Start is called before the first frame update
    void Start()
    {
         
    }
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {

            if (i<inventory.maxInventoryAmount)
            {
                //slots[i].Additem(inventory.AddPickUp);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

   
}
