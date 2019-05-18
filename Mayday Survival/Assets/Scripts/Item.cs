using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//tell unity how to create new items
[CreateAssetMenu(fileName = "NewItem",menuName = "InventoryItem")]
public class Item : ScriptableObject
{/* May 2019 - Timothy Kwan */
    //set the item name
    public string ItemName = "Something";
    //create an icon for the item
    public Sprite icon = null;

    public virtual void Use()
    {
        //use the item
        //things that happen when use
        Debug.Log("Using" + name);
    }
}