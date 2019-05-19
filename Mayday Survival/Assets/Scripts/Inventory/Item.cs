using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    //set the item name
    public string ItemName = "Something";
    //create an icon for the item
    public Sprite icon = null;

    public virtual void Use()
    {
        //use the item
        //things that happen when use
        if (gameObject.activeInHierarchy)
        {
            Debug.Log("Using" + name);
        }
        
    }
    public virtual void PickUpItem(Transform pickupPoint)
    {
        Debug.Log("Pick up item" + name);
        bool PickedUp = Inventory.instance.Add(this);
        if (PickedUp)
        {
            transform.position = pickupPoint.position;
            transform.parent = pickupPoint;
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    public virtual void DropItem()
    {
        Debug.Log("Drop item" + name);
        Inventory.instance.Remove(this);
        transform.parent = null;
        gameObject.SetActive(true);

    }
}
