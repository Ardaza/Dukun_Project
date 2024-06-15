using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Transform[] slot;

    private void Start()
    {
        ResetUI();
    }


    public void UpdateUI()
    {
        ResetUI();
        for (int i = 0; i < InventoryManager.Instance.listItem.Count; i++)
        {
            slot[i].gameObject.SetActive(true);
            slot[i].transform.GetChild(0).GetComponent<Image>().sprite =
               InventoryManager.Instance.listItem[i].itemSo.itemIcon;
            slot[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
               InventoryManager.Instance.listItem[i].stock.ToString();
        }
    }

    public void ResetUI()
    {
        foreach (var slotItem in slot)
        {
            slotItem.gameObject.SetActive(false);
        }
    }
}
