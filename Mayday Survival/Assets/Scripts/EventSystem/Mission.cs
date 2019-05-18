using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Mission : Event
{
    [System.Serializable]
    public struct Requirment
    {
        public int number;
        //public Item item;
    }

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

}
