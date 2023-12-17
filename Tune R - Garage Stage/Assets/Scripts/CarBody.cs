using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/CarBody")]
public class CarBody : ScriptableObject
{
    private string brand;
    public Sprite sprite;

    [Range (600, 3000)]
    public float weight;

    private int upgradesCount = 0;
    const int UPGRADES_MAX = 2;

    void Awake()
    {
        brand = this.name;
    }

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
        return weight-(upgradesCount * (weight/8f) );
    }
}
