using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Transmission")]
public class Transmission : Item
{
    [Range(0, 100)]
    public float efficiencyInPercents;
}
