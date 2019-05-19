using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    //public GameObject inventoryUI;

    Inventory inventory;

    InventorySlot[] slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        //subscribe to the event by adding the UpdateUI
        inventory.OnItemChangeCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }*/
    }

    //update the user interface whenever there is a add or remove item in the inventory
    void UpdateUI()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
                if(inventory.selectedItem == i)
                {
                    slots[i].selected.enabled = true;
                }
                else
                {
                    slots[i].selected.enabled = false;
                }
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
