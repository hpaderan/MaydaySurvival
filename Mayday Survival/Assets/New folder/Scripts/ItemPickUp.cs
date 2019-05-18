using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : Interactable
{
    public Item item;
    public override void interact()
    {
        /*this goes to the interact function in Interactable class
         and execute whatever is in there*/
        base.interact();

        PickUpItem();
    }

    void PickUpItem()
    {
        Debug.Log("Pick up item" + item.ItemName);
        bool PickedUp = Inventory.instance.ADD(item);
        if (PickedUp)
        {
            Destroy(gameObject);
        }
    }
}
