using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReward : Reward
{
    public GameObject item;

    public override void Activate()
    {
        base.Activate();
        GameObject go = Instantiate(item, FindObjectOfType<Dropbox>().dropPoint.position, Quaternion.identity);
    }
}
