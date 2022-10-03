using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BSODEvent : GameEvent
{
    public GameObject bsod;

    public override void StartEvent(float time)
    {
        base.StartEvent(time);
        bsod.SetActive(true);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();
        bsod.SetActive(false);
    }
}
