using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Event
{
    [SerializeField]
    public float chance;
    [SerializeField]
    public bool canPlay;
    [SerializeField]
    public bool oneUse;
    //public Effect eventEffect;

    public virtual void UpdateEvent()
    {

    }
    public virtual void PlayEvent()
    {
        if(oneUse)
        {
            canPlay = false;
        }
    }
    public virtual void FinishEvent()
    {
        Debug.Log("Finishing Event");
        //EventManager.instance.FinishEvent(this);
    }
}
