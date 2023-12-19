using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;
using System.Xml.Serialization;


public class InventoryItem : MonoBehaviour, IPointerClickHandler
{
    Image itemIcon;
    public CanvasGroup canvasGroup {get; private set;}
    public Durability durability;
    public Item myItem {get; set;}
    public InventorySlot activeSlot {get; set;}

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        itemIcon = GetComponent<Image>();
    }

    public void Initialize(Item item, InventorySlot parent)
    {
        activeSlot = parent;
        activeSlot.myInventoryItem = this;
        //myItem = ScriptableObject.CreateInstance<Item>();
        myItem = Instantiate(item);
        itemIcon.sprite = item.sprite;
        durability.maxPower = item.maxPower;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            Inventory.Singleton.SetCarriedItem(this);
        }
    }
}
