using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public Item data;

    public void PickUp()
    {
        Inventory.TryAddItem(data);
        Destroy(gameObject);
    }
}
