using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Disaster : Event
{
    public string title = "BAD STUFF HAPPENING!";
    public float disasterTime;
    private float disasterStamp;
    public int damage;
    public GameObject disasterEffect;
    public enum DisasterType { dmgReward, delayRescue }
    public DisasterType disasterType;

    public List<Reward> rewards;

    public override bool TryEvent()
    {
        return base.TryEvent();
    }
    public override void PlayEvent()
    {
        Debug.Log("Playing Disaster");
        switch (disasterType)
        {
            case DisasterType.dmgReward:
                EventManager.instance.AttackReward(damage);
                break;
            case DisasterType.delayRescue:
                EventManager.instance.DelayRescue(damage);
                break;
        }
        for (int i = 0; i < rewards.Count; i++)
        {
            rewards[i].Activate();
            EventManager.instance.FinishEvent(this);
        }
        base.PlayEvent();
        disasterStamp = Time.time;
    }
    public override void FinishEvent()
    {
        base.FinishEvent();  
        Debug.Log("Disaster over!");
    }

    public override void UpdateEvent()
    {
        base.UpdateEvent();
        if(disasterStamp + disasterTime < Time.time)
        {
            FinishEvent();
        }
    }
}
