using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrinterEvent : GameEvent
{
    public GameObject smokeObject;

    public override void StartEvent(float time)
    {
        base.StartEvent(time);

        smokeObject.SetActive(true);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();

        smokeObject.SetActive(false);
    }
}
