using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    //this variable share by all instances of the class
    public static Inventory instance;

    void Awake()
    {
        //each time when called Inventory.instance it will always come here
        if (instance != null)
        {
            Debug.LogWarning("Multiple Inventory occur");
            return;
        }
        instance = this;
    }

    #endregion
    //this allow unity to know when has the inventory add or remove an item
    public delegate void OnItemChange();
    public OnItemChange OnItemChangeCallBack;
    //default space for the inventory
    public int space = 10;
    //Item list
    public List<Item> items = new List<Item>();

    //add items into the inventory
    public bool ADD (Item item)
    {
        
            if (items.Count >= space)
            {
                Debug.Log("Not enough room for inventory");
                return false;
            }
            items.Add(item);
            //when the event is triggered this will update the UI
            if (OnItemChangeCallBack != null)
            {
                OnItemChangeCallBack.Invoke();
            }
        return true;
    }

    //remove the item for the items list
    public void Remove(Item item)
    {
        items.Remove(item);

        if (OnItemChangeCallBack != null)
        {
            OnItemChangeCallBack.Invoke();
        }
    }
}
