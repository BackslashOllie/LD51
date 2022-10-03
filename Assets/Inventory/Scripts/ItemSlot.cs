using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlot : MonoBehaviour
{
    [SerializeField]
    Image icon;

    [SerializeField]
    TextMeshPro label;

    public void Set(Item item)
    {
        icon.sprite = item.itemIcon;
        label.text = item.itemName;
    }
}
