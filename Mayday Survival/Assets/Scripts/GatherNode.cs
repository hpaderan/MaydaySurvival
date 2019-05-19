using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherNode : MonoBehaviour
{
    public int minDrop = 3;
    public int maxDrop = 12;

    public float health = 15f;
    public Item droppedItem;
    public Item requiredItem;

    public void Damage(float dmg, Item usedItem)
    {
        if (usedItem ==requiredItem || requiredItem == null)
        {
            //spawn a particle effect
            //make a sound
            health -= dmg;
            if (health <= 0)
            {
                Explode();
            }
        }
    }
    public virtual void Explode()
    {
        if (droppedItem != null)
        {
            int drop = Random.Range(minDrop, maxDrop + 1);
            for (int i = 0; i < drop; i++)
            {
                Instantiate(droppedItem, transform.position, Quaternion.identity);
            }
        }
        //Big particle and sound
        Destroy(gameObject);
        
    }

}
