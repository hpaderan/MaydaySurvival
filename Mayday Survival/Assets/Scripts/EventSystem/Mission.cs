﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission : Event
{
    public string title = "Get the Stuff";
    public List<Requirment> requirments = new List<Requirment>();
    public List<ItemReward> iRewards;
    public List<BuildReward> bRewards;
    public List<EventDeck> dRewards;
    public bool TryEvent()
    {
        if (EventManager.instance.CheckEvent(this))
        {
            if (canPlay)
            {
                if (Random.Range(1, 101) <= chance)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    public override void PlayEvent()
    {
        Debug.Log("Starting Mission :");
        base.PlayEvent();
    }
    public override void FinishEvent()
    {
        Debug.Log("Mission Complete");
        for (int i = 0; i < iRewards.Count; i++)
        {
            iRewards[i].Activate();
        }
        for (int i = 0; i < bRewards.Count; i++)
        {
            bRewards[i].Activate();
        }
        for (int i = 0; i < dRewards.Count; i++)
        {
            EventManager.instance.AddDeck(dRewards[i]);
        }
        EventManager.instance.FinishEvent(this);

    }
    public override void UpdateEvent()
    {
        base.UpdateEvent();
        int completed = 0;
        Dropbox db = EventManager.instance.GetComponent<Dropbox>();
        for (int i = 0; i < requirments.Count; i++)
        {
            requirments[i].number = db.GetItemNum(requirments[i].item);
            if (requirments[i].number >= requirments[i].numberNeeded)
            {
                completed++;
            }
        }

        if (completed == requirments.Count)
        {
            db.ClearMissionItems(this);
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
