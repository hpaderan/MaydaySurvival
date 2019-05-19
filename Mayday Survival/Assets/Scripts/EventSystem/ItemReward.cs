using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReward : Reward
{
    public Item item;

    public override void Activate()
    {
        base.Activate();
        GameObject go = Instantiate(item.gameObject);
        //Inventory.instance.Add();
    }
}
