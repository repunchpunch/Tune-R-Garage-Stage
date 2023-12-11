using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Engine")]
public class Engine : Item
{
    public float basePower;
    private float currentUpradeInPercents;
    private float boughtUpgradeInPercents;
    
    public void setUpgradeInPercents(float upgrade)
    {
       currentUpradeInPercents = Mathf.Clamp(upgrade, 0, boughtUpgradeInPercents);
    }

    public void buyUpgradeAddPercents(float upgrade)
    {
        boughtUpgradeInPercents += upgrade;
    }

    public float getCurrentPower()
    {
        return basePower*(100f+currentUpradeInPercents);
    }
}
