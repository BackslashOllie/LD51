using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon, fillBar;

    private GameEvent _linkedEvent;

    private void Update()
    {
        if (_linkedEvent)
        {
            if (_linkedEvent.notification) fillBar.fillAmount = _linkedEvent.notification.fillAmount;
        }
        else
        {
            fillBar.fillAmount = 0;
        }
    }

    public void Fill(Item item)
    {
        if (Guid.TryParse(item.itemId, out Guid parsedGuid))
        {
            _linkedEvent = GameEventManager.Instance.allEvents.FirstOrDefault(e => e.objectIDRequired == item.itemId);
        }
        icon.enabled = true;
        icon.sprite = item.itemIcon;
    }

    public void Empty()
    {
        icon.enabled = false;
        _linkedEvent = null;
    }
}
