using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region singleton
    public static EventManager instance = null;

    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public List<Event> allEvents;
    public List<Event> finishedEvents;

    public List<BuildReward> rewards;

    public struct ActiveEvent
    {
        public Event aEvent;
        public float timeStamp;
    }

    public List<ActiveEvent> activeEvents;

    public float eventWait = 10f;

    // Update is called once per frame
    void Update()
    {
        CheckDeck();
        UpdateActive();
    }
    private void UpdateActive()
    {
        for (int i = 0; i < activeEvents.Count; i++)
        {
            if (activeEvents[i].aEvent != null)
            {
                activeEvents[i].aEvent.UpdateEvent();
            }
        }
    }
    /// <summary>
    /// Attack a list of the build rewards that are available
    /// </summary>
    /// <param name="damage">The amount done to the structure</param>
    public void AttackReward(int damage)
    {
        if (rewards.Count > 0)
        {
            int i = Random.Range(0, rewards.Count);
            Debug.Log("Reward " + rewards[i].name + " taking damage.");
            rewards[i].health -= damage;
            if (rewards[i].health <= 0)
            {
                rewards[i].Kill();
                UnFinishEvent(rewards[i]);
            }
        }

    }
    /// <summary>
    /// Go back to a previous one use only event and return it to the deck, upon the destruction of it's reward
    /// </summary>
    /// <param name="br">The reward that got destroyed</param>
    private void UnFinishEvent(BuildReward br)
    {
        for (int i = 0; i < finishedEvents.Count; i++)
        {
            if ((Mission)finishedEvents[i] != null)
            {
                Mission m = (Mission)finishedEvents[i];
                for (int j = 0; j < m.rewards.Count; j++)
                {
                    if (m.rewards[j] == br)
                    {
                        finishedEvents[i].canPlay = true;
                    }
                }
            }
            
        }
    }
    /// <summary>
    /// Delay the rescue of the player(s) by an amount of time
    /// </summary>
    /// <param name="timeIncrease">The amount of time for the increase</param>
    public void DelayRescue(int timeIncrease)
    {
        //Add delay to end game
        //TODO
    }
    /// <summary>
    /// Finish an Event, either by a disaster timing out or a mission being complete
    /// </summary>
    /// <param name="e">The event that finished</param>
    public void FinishEvent(Event e)
    {
        for (int i = 0; i < activeEvents.Count; i++)
        {
            if (activeEvents[i].aEvent == e)
            {
                if(e.oneUse) { finishedEvents.Add(e); }
                activeEvents[i] = new ActiveEvent { aEvent = null, timeStamp = Time.time };
            }
        }
    }
    /// <summary>
    /// Check to see if the event is already being played
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public bool CheckEvent(Event e)
    {
        bool containEvent = false;
        for (int i = 0; i < activeEvents.Count; i++)
        {
            if (activeEvents[i].aEvent == e)
            {
                containEvent = true;
            }
        }
        return containEvent;
    }
    /// <summary>
    /// Draw a new event from the list to become and active event
    /// </summary>
    /// <returns>The new event becoming active</returns>
    private Event DrawNewEvent()
    {
        Event e = null;
        if (allEvents.Count > 0)
        {
            do
            {
                int i = Random.Range(0, allEvents.Count);
                if (allEvents[i].TryEvent())
                {
                    e = allEvents[i];
                }
            } while (e == null);
        }
        else
        {
            return null;
        }
        return e;
    }
    /// <summary>
    /// Check the deck for any issues, if there are no active events or if any null events have timed out
    /// </summary>
    private void CheckDeck()
    {
        //Check if no active events
        if (allEvents.Count > 0)
        {
            do
            {
                int i = Random.Range(0, allEvents.Count);
                if (allEvents[i].TryEvent())
                {
                    ActiveEvent newE = new ActiveEvent
                    {
                        aEvent = allEvents[i],
                        timeStamp = Time.time
                    };

                    activeEvents.Add(newE);
                }
            }
            while (activeEvents.Count < 1);
        }
        //Check for timed out active events
        for (int i = 0; i < activeEvents.Count; i++)
        {
            if (Time.time - activeEvents[i].timeStamp >= eventWait && activeEvents[i].aEvent == null)
            {
                activeEvents[i] = new ActiveEvent { aEvent = DrawNewEvent() };
            }
        }
        
    }
    /// <summary>
    /// Add a new deck to the list of events
    /// </summary>
    /// <param name="deck">The new deck</param>
    public void AddDeck(EventDeck deck)
    {
        for (int i = 0; i < deck.missions.Count; i++)
        {
            deck.events.Add(deck.missions[i]);
        }
        for (int i = 0; i < deck.disaster.Count; i++)
        {
            deck.events.Add(deck.disaster[i]);
        }
        foreach (Event e in deck.events)
        {
            allEvents.Add(e);
        }
    }
    /// <summary>
    /// Remove a deck from the list of events
    /// </summary>
    /// <param name="deck">The deck being removed</param>
    public void RemoveDeck(EventDeck deck)
    {
        foreach (Event e in deck.events)
        {
            allEvents.Remove(e);
        }
    }
}
