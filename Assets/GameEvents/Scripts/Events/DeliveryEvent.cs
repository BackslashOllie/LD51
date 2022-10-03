using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryEvent : GameEvent
{
    private Elevator[] _elevators;
    public string recipientName;

    public override void StartEvent(float time)
    {
        string guid = Guid.NewGuid().ToString();
        _elevators = FindObjectsOfType<Elevator>();
        Elevator elevator = _elevators[Random.Range(0, _elevators.Length)];
        elevator.NewDelivery(guid, recipientName);
        objectIDRequired = guid;
        base.StartEvent(time);
    }

    public override void FailEvent()
    {
        base.FailEvent();
        if (Inventory.Instance.HasItem(objectIDRequired)) Inventory.Instance.RemoveItem(objectIDRequired);
        DeliveryBox box = (DeliveryBox) Inventory.Instance.FindCollectableInScene(objectIDRequired);
        if (box) Destroy(box.gameObject);
    }
}
