using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EventManager : MonoBehaviour
{
    #region singleton
    public static EventManager instance = null;
    public List<EventDeck> startingDecks;
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

    public int activeNum = 3;
    public List<Mission> allMissions;
    public List<Disaster> allDisasters;

    public List<Mission> finishedEvents;

    public List<BuildReward> rewards;
    public List<Mission> activeMissions;

    public float eventWait = 10f;
    private void Start()
    {
        activeMissions = new List<Mission>();

        foreach (EventDeck d in startingDecks)
        {
            AddDeck(d);
        }
    }
    // Update is called once per frame
    void Update()
    {
        CheckDeck();
        UpdateActive();
    }
    private void UpdateActive()
    {
        if (activeMissions != null)
        {
            for (int i = 0; i < activeMissions.Count; i++)
            {
                if (activeMissions[i] != null)
                {
                    activeMissions[i].UpdateEvent();
                }
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
                for (int j = 0; j < m.bRewards.Count; j++)
                {
                    if (m.bRewards[j] == br)
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
    public void FinishEvent(Mission e)
    {
        for (int i = 0; i < activeMissions.Count; i++)
        {
            if (activeMissions[i] == e)
            {
                if (e.oneUse)
                {
                    finishedEvents.Add(e);
                    allMissions.Remove(e);
                }
            }
        }
    }
    /// <summary>
    /// Check to see if the event is already being played
    /// </summary>
    /// <param name="e"></param>
    /// <returns></returns>
    public bool CheckEvent(Mission e)
    {
        bool notFoundEvent = true;
        if (activeMissions != null)
        {
            for (int i = 0; i < activeMissions.Count; i++)
            {
                if (activeMissions[i] == e)
                {
                    notFoundEvent = false;
                }
            }
        }
        return notFoundEvent;
    }
    /// <summary>
    /// Draw a new event from the list to become and active event
    /// </summary>
    /// <returns>The new event becoming active</returns>
    private Mission DrawNewEvent()
    {
        Mission e = null;
        if (allMissions.Count > 0)
        {
            for (int i = 0; i < allMissions.Count; i++)
            {
                if (allMissions[i].TryEvent())
                {
                    e = allMissions[i];
                }
            }
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
        if (activeMissions.Count == 0)
        {
            activeMissions.Add(DrawNewEvent());
        }
        else if (finishedEvents.Count > 0)
        {
            if (activeMissions.Count < activeNum)
            {
                activeMissions.Add(DrawNewEvent());
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
            allMissions.Add(deck.missions[i]);
        }
        for (int i = 0; i < deck.disaster.Count; i++)
        {
            allDisasters.Add(deck.disaster[i]);
        }
    }
    /// <summary>
    /// Remove a deck from the list of events
    /// </summary>
    /// <param name="deck">The deck being removed</param>
    public void RemoveDeck(EventDeck deck)
    {
        foreach (Mission e in deck.missions)
        {
            allMissions.Remove(e);
        }
        foreach (Disaster d in deck.disaster)
        {
            allDisasters.Remove(d);
        }
    }
}
