using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckReward : Reward
{
    public EventDeck deck;
    public override void Activate()
    {
        base.Activate();
        EventManager.instance.AddDeck(deck);
    }
}
