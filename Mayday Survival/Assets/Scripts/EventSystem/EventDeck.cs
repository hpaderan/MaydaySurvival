using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Event Deck", menuName = "Event Deck", order = 0)]
public class EventDeck : ScriptableObject
{
    /// <summary>
    /// Turned on and off by Event manager during certain conditions (aka nighttime)
    /// </summary>
    [HideInInspector]
    public bool isAvailable = true;
    /// <summary>
    /// The list of events in this deck
    /// </summary>
    [SerializeField]
    public List<Mission> missions;
    [SerializeField]
    public List<Disaster> disaster;

    public void RemoveMission(Mission m)
    {
        if (missions.Contains(m)) { missions.Remove(m); }
    }

}
