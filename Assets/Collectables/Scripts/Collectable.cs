using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MarkerTrigger
{
    public Item data;

    protected void Start()
    {
        base.Start();
        data.startPosition = transform.position;
    }
    
    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Inventory.Instance.collectablesInRange.Add(this);
    }

    protected void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if (other.gameObject.tag == "Player")
            Inventory.Instance.collectablesInRange.Remove(this);
    }
    
    public void Pickup()
    {
        Inventory.TryAddItem(data);

        if (Inventory.Instance.HasItem(data.itemId))
        {
            marker.HideMarker();
            Destroy(gameObject);
        }
        
    }
}
