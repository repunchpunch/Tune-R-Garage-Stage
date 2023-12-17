using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;

    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField] Item[] items;

    [SerializeField] Button raceBtn;

    void Awake()
    {
        Singleton = this;
        raceBtn.onClick.AddListener(delegate { SpawnItemForRace(); });
    }
    
    public void SpawnItemForRace(Item item = null)
    {
        if (!CarBuilder.Instance.AreAllPartsPresent()) return;
        Item _item = item;
        if(_item == null)
        {
            int random = Random.Range(0, items.Length);
            _item = items[random];
        }
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if(inventorySlots[i].myItem == null)
            {
                Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(_item, inventorySlots[i]);
                break;
            }
        }
    }

    public void SpawnItemForWork()
    {
        List<Item> stockItems = new List<Item>();
        foreach (Item i in items)
        {
            if (i.quality == 0)
            {
                stockItems.Add(i);
            }
        }
        int random = Random.Range(0, stockItems.Count);
        Item item = stockItems[random];

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            if(inventorySlots[i].myItem == null)
            {
                Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(item, inventorySlots[i]);
                break;
            }
        }
    }

    void Update()
    {
        if (carriedItem == null) return;

        carriedItem.transform.position = Input.mousePosition;
    }
    
    public void SetCarriedItem(InventoryItem item)
    {
        // If there's a carried item and it's not compatible with the slot, return
        if(              carriedItem != null 
            && item.activeSlot.myTag != SlotTag.none
            && item.activeSlot.myTag != carriedItem.myItem.itemTag) 
            return;
        
        // If the slot is tagged, remove the current part from the CarBuilder
        if(item.activeSlot.myTag != SlotTag.none)
            CarBuilder.Instance.RemovePartAndBuild(item.activeSlot.myTag);

        // If there's a carried item, place it in the slot
        if(carriedItem != null)
            item.activeSlot.SetItem(carriedItem);

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
    }
}
