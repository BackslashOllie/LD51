using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : Singleton<GameEventManager>
{

    public GameEvent[] allEvents;

    List<GameEvent> events = new List<GameEvent>();
    public const float TenSec = 11f;
    float timer;

    public int secondsRemaining = 10;

    public static void TryGetNewEvent()
    {
        Instance.GetNewEvent();
    }

    public void GetNewEvent()
    {
        List<GameEvent> possibleEvents = new List<GameEvent>();

        for (int i = 0; i < allEvents.Length; i++)
        {
            if(!allEvents[i].isActive && ! allEvents[i].inCoolDown)
            {
                possibleEvents.Add(allEvents[i]);
            }
        }

        if (possibleEvents.Count > 0)
        {
            GameEvent t = possibleEvents[Random.Range(0, possibleEvents.Count)];
            AddNewEvent(t);
        }
    }

    public void AddNewEvent(GameEvent ge)
    {
        ge.StartEvent(Time.time);
        events.Add(ge);
    }


    public static void RemoveEvent(GameEvent ge)
    {
        Instance.events.Remove(ge);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        secondsRemaining = Mathf.FloorToInt(TenSec - timer);
        LogManager.IncreaseTimeSurvived(Time.deltaTime);

        if(timer > TenSec)
        {
            GetNewEvent();
            timer = 0;
        }

        for (int i = 0; i < events.Count; i++)
        {
            events[i].UpdateEvent(Time.deltaTime);
        }

        LogManager.UpdateHighScoreText();
    }
}



