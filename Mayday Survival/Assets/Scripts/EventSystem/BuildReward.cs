using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildReward : Reward
{
    public GameObject buildEffect;
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
    }

    public void Kill()
    {
        health = 0;
        //anim blow up
        Destroy(gameObject, 2f);
        Debug.Log(name + " destroyed in event");
    }
}
