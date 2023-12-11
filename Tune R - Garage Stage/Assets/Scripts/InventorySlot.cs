using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myItem {get; set;}

    public SlotTag myTag;

    public delegate void ItemAddedDelegate(InventoryItem item);
    public static event ItemAddedDelegate OnItemAdded;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(Inventory.carriedItem == null) return;
            if(myTag != SlotTag.none && Inventory.carriedItem.myItem.itemTag != myTag) return;
            SetItem(Inventory.carriedItem);
        }
    }

    public void SetItem(InventoryItem item)
    {
        Inventory.carriedItem = null;

        item.activeSlot.myItem = null;

        myItem = item;
        myItem.activeSlot = this;
        myItem.transform.SetParent(transform);
        myItem.transform.localPosition = Vector3.zero;
        myItem.canvasGroup.blocksRaycasts = true;

        if(myTag != SlotTag.none)
        {
            OnItemAdded?.Invoke(item);
            //Debug.Log("Signal");
        }

        // if(myTag != SlotTag.none)
        // {
        //     Inventory.Singleton.EquipEquipment(myTag, myItem);
        // }
    }
}
