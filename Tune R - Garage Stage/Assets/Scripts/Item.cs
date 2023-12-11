using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum SlotTag {none, engine, transmission, turbo, suspension, tires, brakes}

public class Item : ScriptableObject
{
    public Sprite sprite;
    public SlotTag itemTag;
    public float maxPower;
    public int quality;
    protected float durabilityLeft = 100f;

    // [Header("If the item can be equipped")]
    // public GameObject equipmentPrefab;

    public float BreakEventEquation(float currentPower)
    {
        return 0.9f*(currentPower/maxPower)+0.1f + ((100f-durabilityLeft)/100f);
    }

    public bool WillBreak(float currentPower)
    {
        float chance = BreakEventEquation(currentPower);
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        return randomValue < chance;
    }
}
