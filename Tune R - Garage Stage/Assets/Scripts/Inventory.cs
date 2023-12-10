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

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;

    void Awake()
    {
        Singleton = this;
        giveItemBtn.onClick.AddListener(delegate { SpawnInventoryItem(); });
    }
    
    public void SpawnInventoryItem(Item item = null)
    {
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

    void Update()
    {
        if (carriedItem == null) return;

        carriedItem.transform.position = Input.mousePosition;
    }
    
    public void SetCarriedItem(InventoryItem item)
    {
        if(carriedItem != null)
        {
            if(item.activeSlot.myTag != SlotTag.none && item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
            item.activeSlot.SetItem(carriedItem);
        }

        if(item.activeSlot.myTag != SlotTag.none)
        {
            EquipEquipment(item.activeSlot.myTag, null);
        }

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
    }

    public void EquipEquipment (SlotTag tag, InventoryItem item = null)
    {
        switch (tag)
        {
            case SlotTag.engine:
                break;
            case SlotTag.transmission:
                break;
            case SlotTag.turbo:
                break;
            case SlotTag.suspension:
                break;
            case SlotTag.tires:
                break;
            case SlotTag.brakes:
                break;
        }
    }
}
