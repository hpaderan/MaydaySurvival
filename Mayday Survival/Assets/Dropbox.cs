using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropbox : MonoBehaviour
{
    private EventManager em;
    public Transform dropPoint;
    public List<Item> items;
    private void Start()
    {
        em = EventManager.instance;
        dropPoint = transform.GetChild(0);
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

    public void ClearMissionItems(Mission m)
    {
        for (int i = 0; i < m.requirments.Count; i++)
        {
            int num = GetItemNum(m.requirments[i].item);
            for (int j = 0; j < items.Count; j++)
            {
                if (items[j].ItemName == m.requirments[i].item.ItemName && num != 0)
                {
                    items.Remove(items[j]);
                    num--;
                }
            }
        }
    }
}
