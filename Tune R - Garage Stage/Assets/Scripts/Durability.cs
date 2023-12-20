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
        return 0.5f*(currentPower / maxPower) + ((100f - durabilityLeft) / 100f);
    }

    public bool WillBreak(float currentPower)
    {
        float chance = BreakEventEquation(currentPower);
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        Debug.Log($"Random{randomValue} < Chance{Mathf.Pow(chance,16)}");
        return randomValue < Mathf.Pow(chance, 16);
    }

    public void GetDamage(float currentPower)
    {
        durabilityLeft -= BreakEventEquation(currentPower) * 10f;
        bar.value = 1 - durabilityLeft / 100f;
    }
}
