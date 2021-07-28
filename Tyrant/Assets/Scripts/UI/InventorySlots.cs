using UnityEngine;
using UnityEngine.UI;

public class InventorySlots : MonoBehaviour
{
    public Image icon;
    public Button removeButton;
   
    Inventory inventory;
    private void Start()
    {
       
    }
    public void Additem(Inventory newPickup)
    {
        inventory = newPickup;
        //icon.sprite = inventory.AddPickUp;
        icon.enabled = true;                
        removeButton.interactable = true;
    }
    public void ClearSlot()
    {
        icon.sprite = null;
        icon.enabled = false;
    }
}
