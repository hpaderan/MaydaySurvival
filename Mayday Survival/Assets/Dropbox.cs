using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropbox : MonoBehaviour
{
    private EventManager em;
    public List<Item> items;
    private void Start()
    {
        em = EventManager.instance;
    }
    public void DepositItem(Item item)
    {
        items.Add(item);
        Inventory.instance.Remove(item);
        item.gameObject.SetActive(false);
    }
    public int GetItemNum(Item item)
    {
        int x = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ItemName == item.ItemName)
            {
                x++;
            }
        }
        return x;
    }
}
