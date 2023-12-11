using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Car : ScriptableObject
{
    private float power;
    private double chassis;
    private double reliability;

    public void Initialize ( 
                float power,
                double chassis,
                double reliability
               )
    {
        this.power = power;
        this.chassis = chassis;
        this.reliability = reliability;
        Debug.Log($"{power} {chassis} {reliability}");
    }
}
