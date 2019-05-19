using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropbox : MonoBehaviour
{
    private EventManager em;
    private void Start()
    {
        em = EventManager.instance;
    }
    public bool DepositItem(Item item)
    {
        bool deposited = false;
        for (int i = 0; i < em.activeEvents.Count; i++)
        {
            if ((Mission)em.activeEvents[i].aEvent != null)
            {
                Mission m = (Mission)em.activeEvents[i].aEvent;
                for (int j = 0; j < m.requirments.Count; j++)
                {
                    if(m.requirments[i].item == item)
                    {
                        m.requirments[i].number++;
                        Inventory.instance.Remove(item);
                        deposited = true;
                    }
                }
            }
        }

        return deposited;
    }
}
