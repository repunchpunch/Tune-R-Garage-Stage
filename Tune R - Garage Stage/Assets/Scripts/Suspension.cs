using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Suspension")]
public class Suspension : Item
{
    [Range(0, 100)]
    public float efficiencyInPercents;

    public void Rebuild()
    {
        float difRepair = 100f - durabilityLeft;
        durabilityLeft += difRepair * UnityEngine.Random.Range(0.4f, 0.8f);
    }
}
