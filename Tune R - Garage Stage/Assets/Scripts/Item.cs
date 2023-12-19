using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum SlotTag {none, engine, transmission, turbo, suspension, tires, brakes}

[Serializable]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public SlotTag itemTag;
    public float maxPower;
    public int quality;
}
