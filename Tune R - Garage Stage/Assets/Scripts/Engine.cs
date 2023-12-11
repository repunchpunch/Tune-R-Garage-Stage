using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Engine")]
public class Engine : Item
{
    public float basePower;
    private float currentUpradeInPercents = 0f;
    private int upgradesCount = 0;
    const int UPGRADES_MAX = 4;
    
    public void setUpgradeInPercents(float upgrade)
    {
        float upgradesCap = basePower*upgradesCount;
        currentUpradeInPercents = Mathf.Clamp(upgrade, 0, upgradesCap);
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

    public float getCurrentEnginePower()
    {
        return basePower*(100f+currentUpradeInPercents);
    }
}
