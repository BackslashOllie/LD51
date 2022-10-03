using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_InventoryDisplay : MonoBehaviour
{
    public ItemSlot slotPrefab;

    private void Start()
    {
        Inventory.OnInventoryChange += OnUpdate;
    }

    public void OnUpdate()
    {
        // clear 
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i));
        }

        //draw
        foreach (Item item in Inventory.Instance.items)
        {
            AddSlot(item);
        }
    }

    public void AddSlot(Item i)
    {
        ItemSlot itemSlot = Instantiate(slotPrefab);
        itemSlot.Set(i);
        itemSlot.transform.SetParent(this.transform);

    }
}
