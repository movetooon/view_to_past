using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventHandler : MonoBehaviour
{
    [SerializeField] private List<UnityEvent> events;
    private int currentEvent = 0;

    public void InvokeEvents(int eventsCount)
    {
        for(int i = currentEvent; i < (currentEvent + eventsCount); i++)
        {
            events[i].Invoke();
        }
        currentEvent+=eventsCount;
    }

    /*
    public void Init()
    {
        foreach (GameEvent e in events)
        {
            e.Init();
        }
    }
    */
}
