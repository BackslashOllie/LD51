using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEvent : GameEvent
{
    public GameObject fireObject;

    public override void StartEvent(float time)
    {
        base.StartEvent(time);

        fireObject.SetActive(true);
    }

    public override void CompleteEvent()
    {
        base.CompleteEvent();

        fireObject.SetActive(false);
    }
}
