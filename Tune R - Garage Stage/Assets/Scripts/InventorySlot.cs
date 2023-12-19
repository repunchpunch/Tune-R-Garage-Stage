using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    public InventoryItem myInventoryItem {get; set;}

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

        //Debug.Log(item.myItem.ToString());
        Inventory.carriedItem = null;

        item.activeSlot.myInventoryItem = null;

        //Durability.GetDamage += Inventory.OnRacing;

        myInventoryItem = item;
        myInventoryItem.activeSlot = this;
        myInventoryItem.transform.SetParent(transform);
        myInventoryItem.transform.localPosition = Vector3.zero;
        myInventoryItem.canvasGroup.blocksRaycasts = true;

        if (myTag != SlotTag.none)
        {
            OnItemAdded?.Invoke(item);
            Inventory.OnRacing += myInventoryItem.durability.GetDamage;
        }
    }

    /*public void TakePartDamage()
    {
        if (*//*myTag != SlotTag.none &&*//* myInventoryItem != null)
        {
            myInventoryItem.durability.GetDamage(CarBuilder.Instance.CalculatePower());
            myInventoryItem.bar.value = 1 - myInventoryItem.durability.durabilityLeft/100f;
            CarBuilder.Instance.BuildCarAndUpdateBars();
        }
    }*/
}
