using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Tires")]
public class Tires : Item
{
    [Range(0, 100)]
    public float efficiencyInPercents;
}
