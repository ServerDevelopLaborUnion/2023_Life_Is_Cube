using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private List<InventorySlot> inventorySlots = new List<InventorySlot>();

    private void Awake()
    {
        GetComponentsInChildren<InventorySlot>(inventorySlots);
    }

    public void SetItem(ItemDataSO itemData)
    {
        for(int i = 0; i < inventorySlots.Count; i++)
        {
            if(inventorySlots[i].Empty)
            {
                inventorySlots[i].SetItem(itemData);
                break;
            }
        }
    }

    // public void RemoveItem(int index)
    // {
    //     if(index >= inventorySlots.Count)
    //         return;

    //     inventorySlots[index]?.RemoveItem();
    // }

    // public void RemoveLastItem()
    // {
    //     int index = 0;

    //     for(int i = 0; i < inventorySlots.Count; i++)
    //     {
    //         if(inventorySlots[i].Empty)
    //         {
    //             index = i;
    //             break;
    //         }
    //     }

    //     RemoveItem(index);
    // }
}
