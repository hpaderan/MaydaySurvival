using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission : Event
{
    public string title = "Get the Stuff";
    public List<Requirment> requirments = new List<Requirment>();
    public List<Reward> rewards;
    public override bool TryEvent()
    {
        return base.TryEvent();
    }
    public override void PlayEvent()
    {
        Debug.Log("Starting Mission :");
        base.PlayEvent();
    }
    public override void FinishEvent()
    {
        Debug.Log("Mission Complete");
        for (int i = 0; i < rewards.Count; i++)
        {
            rewards[i].Activate();
            EventManager.instance.FinishEvent(this);
        }
    }
    public override void UpdateEvent()
    {
        base.UpdateEvent();
        int completed = 0;
        for (int i = 0; i < requirments.Count; i++)
        {
            if(requirments[i].number >= requirments[i].numberNeeded)
            {
                completed++;
            }
        }

        if(completed == requirments.Count)
        {
            FinishEvent();
        }
    }
}

[System.Serializable]
public class Requirment
{
    [HideInInspector]
    public int number;
    public int numberNeeded;
    public Item item;
}
