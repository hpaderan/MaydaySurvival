using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildReward : Reward
{
    public GameObject buildEffect;
    public EventDeck eventDeck;
    private Animator anim;
    public float health;
    private float maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        isActive = false;
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
    }

    public override void Activate()
    {
        base.Activate();
        EventManager.instance.rewards.Add(this);
        EventManager.instance.AddDeck(eventDeck);
    }

    public void Kill()
    {
        health = 0;
        //anim blow up
        Destroy(gameObject, 2f);
        Debug.Log(name + " destroyed in event");
        EventManager.instance.RemoveDeck(eventDeck);
    }
}
