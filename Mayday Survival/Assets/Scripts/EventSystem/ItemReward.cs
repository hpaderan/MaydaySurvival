using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReward : Reward
{
    public Item item;

    public override void Activate()
    {
        base.Activate();
        GameObject go = Instantiate(item.gameObject, FindObjectOfType<PlayerScript>().pickupPoint.position, Quaternion.identity);
        go.transform.parent = FindObjectOfType<PlayerScript>().pickupPoint;
        go.SetActive(false);
        Inventory.instance.Add(item);
    }
}
