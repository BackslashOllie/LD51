using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : Singleton<Inventory>
{
    public Item[] items = new Item[6];
    public InventorySlot[] slots = new InventorySlot[6];
    public List<Collectable> collectablesInRange = new List<Collectable>(); 

    private void Start()
    {
        StarterAssets.StarterAssetsInputs.Instance.SubscribeToInteractInput(OnInteract);
    }

    private void OnDisable()
    {
        StarterAssets.StarterAssetsInputs.Instance.UnsubscribeToInteractInput(OnInteract);
    }

    private void OnInteract()
    {
        if (collectablesInRange.Count > 0)
        {
            Collectable collectable = collectablesInRange[0];
            collectablesInRange.Remove(collectable);
            collectable.Pickup();
        }
    }
    
    public static void TryAddItem(Item item)
    {
        Instance.AddItem(item);
    }

    public void AddItem(Item item)
    {
        int index = GetFreeSlot();

        if (index < 0 || index > items.Length -1)
            return;

        items[index] = item;
        slots[index].Fill(item);
    }

    int GetFreeSlot()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (String.IsNullOrEmpty(items[i].itemName))
                return i;
        }

        return -1;
    }

    public Item GetItem(string id)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i].itemId == id)
            {
                return items[i];
            }
        }

        return null;
    }

    public int GetItemIndex(string n)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].itemName == n)
            {
                return i;
            }
        }

        return -1;
    }

    public bool HasItem(string id)
    {
        if (id == "") return false;
        return GetItem(id) != null;
    }

    public void RemoveItem(Item item)
    {
        if (HasItem(item.itemId))
        {
            int i = GetItemIndex(item.itemName);
            items[i].itemName = "";
            items[i].itemId = "";
            slots[i].Empty();
        }
    }
    
    public void RemoveItem(string id)
    {
        print(id);

        if (id != "")
        {
            Item item = GetItem(id);
            if (item.itemId != "")
            {
                RemoveItem(item);
            }
        }
    }
    
    public Collectable FindCollectableInScene(string objId)
    {
        Collectable[] items = FindObjectsOfType<Collectable>();
        return items.FirstOrDefault(b => b.data.itemId == objId);
    }

    public static Action OnInventoryChange;
}


