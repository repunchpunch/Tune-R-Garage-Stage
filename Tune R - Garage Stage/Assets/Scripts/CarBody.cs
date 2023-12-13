using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarBody : ScriptableObject
{
    public string brand;
    public Sprite sprite;

    [Range (600, 3000)]
    public float weight;

    public Engine stockEngine;
    public Turbo stockTurbo;
    public Transmission stockTransmission;
    public Suspension stockSuspension;
    public Tires stockTires;
    public Brakes stockBraks;

    private int upgradesCount = 0;
    const int UPGRADES_MAX = 2;

    public bool buyUpgrade()
    {
        if (upgradesCount < UPGRADES_MAX)
        {
            upgradesCount += 1;
            return true;
        }
        else return false;
    }

    public float getCurrentWeight()
    {
        return weight-(upgradesCount* (weight/8f) );
    }
}
