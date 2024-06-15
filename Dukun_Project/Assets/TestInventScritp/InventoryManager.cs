using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Item> listItem;
    public static InventoryManager Instance;
    public InventoryUI uiInventory;
    public float inventoryCapacity = 4;

    private void Awake()
    {
        Instance = this;
    }

    public void AddItem(ItemSo item)
    {
        Item newItem = new Item();
        newItem.itemSo = item;
        newItem.stock = 1;
        foreach (var itm in listItem)
        {
            if (item == itm.itemSo)
            {
                itm.stock++;
                uiInventory.UpdateUI();
                return;
            }
        }

        if (listItem.Count >= inventoryCapacity)
        {
            Debug.Log("Inventory Full");
        }
        else
        {
            listItem.Add(newItem);
            uiInventory.UpdateUI();
        }
    }

    public void RemoveItem(ItemSo item)
    {
        foreach (var itm in listItem)
        {
            if (item == itm.itemSo)
            {
                if (itm.stock > 0)
                {
                    itm.stock--;
                    if (itm.stock <= 0)
                    {
                        listItem.Remove(itm);
                    }
                    uiInventory.UpdateUI();
                    return;
                }
            }
        }
    }
}

[System.Serializable]
public class Item
{
    public ItemSo itemSo;
    public float stock;
}
