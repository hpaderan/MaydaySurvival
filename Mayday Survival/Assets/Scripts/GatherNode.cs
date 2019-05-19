using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherNode : MonoBehaviour
{
    public int minDrop = 3;
    public int maxDrop = 12;

    public float health = 15f;
    public GameObject droppedItem;
    public Item requiredItem;

    public void Damage(float dmg, Item usedItem)
    {
        if (usedItem.ItemName ==requiredItem.ItemName || requiredItem == null)
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
        //Big particle and sound
        Destroy(gameObject);

        if (droppedItem != null)
        {
            int drop = Random.Range(minDrop, maxDrop + 1);
            for (int i = 0; i < drop; i++)
            {
                float xOffset = Random.Range(-1f, 1f);
                float yOffset = Random.Range(-1f, 1f);
                GameObject go = Instantiate(droppedItem, new Vector3(transform.position.x + xOffset, transform.position.y + 2, transform.position.z + yOffset), Quaternion.identity);
            }
        }   
    }

}
