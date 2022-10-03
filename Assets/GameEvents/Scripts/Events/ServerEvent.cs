using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerEvent : GameEvent
{
    public GameObject sparks, smoke;

    public override void StartEvent(float time)
    {
        base.StartEvent(time);
        sparks.SetActive(true);
        smoke.SetActive(true);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        sparks.SetActive(false);
        smoke.SetActive(false);
    }

}
