using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Turbo")]
public class Turbo : Item
{
    private float currentBoostInPercents = 0f;
    public float maxBoostInPercents;

    public void setBoostInPercents(float boost)
    {
       currentBoostInPercents = Mathf.Clamp(boost, 0, maxBoostInPercents);
    }

    public float getCurrentBoostInPercents()
    {
        return currentBoostInPercents;
    }
}
