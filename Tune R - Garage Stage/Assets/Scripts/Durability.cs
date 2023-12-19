using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Durability : MonoBehaviour
{
    public float maxPower;
    
    public Slider bar;

    public float durabilityLeft = 100f;

    public float BreakEventEquation(float currentPower)
    {
        return 0.9f * (currentPower / maxPower) + 0.1f + ((100f - durabilityLeft) / 100f);
    }

    private bool WillBreak(float currentPower)
    {
        float chance = BreakEventEquation(currentPower);
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        return randomValue < chance;
    }

    public void GetDamage(float currentPower)
    {
        durabilityLeft -= BreakEventEquation(currentPower) * 10f;
        bar.value = 1 - durabilityLeft / 100f;
    }
}
