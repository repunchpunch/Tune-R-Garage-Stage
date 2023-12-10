using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotTag {none, engine, transmission, turbo, suspension, tires, brakes}

[CreateAssetMenu(menuName = "ScriptableObject/Item")]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public SlotTag itemTag;
    public float durabilityLeft;
    public float maxPower;
    public int quality;

    [Header("If the item can be equipped")]
    public GameObject equipmentPrefab;
}
