﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Image pointer;
    public Image selected;
    Item item;
    public Button removeButton;

    //set the icon into the inventory slot and enble the remvoe button
    //for player to remove this item
    public void AddItem(Item newItem)
    {
        item = newItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    /*set the icon and removeButton to disable when the slot is clear*/
    public void ClearSlot()
    {
        item = null;
        pointer.enabled = false;
        selected.enabled = false;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            item.Use();
        }
    }
}