using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Car : ScriptableObject
{
    private float power;
    private double chassis;
    private double reliability;

    // void Start()
    // {
    //     car.Reset();
    // }

    // public void Reset()
    // {
    //     power = 0f;
    //     chassis = 0.0;
    //     reliability = 0.0;
    // }

    public void Initialize ( 
                float power,
                double chassis,
                double reliability
               )
    {
        this.power = power;
        this.chassis = chassis;
        this.reliability = reliability;
        /*Debug.Log(string.Format("Power:{0,10:F4}  Handling:{1,10:F4}  Reliability:{2,10:F4}",
                                power, chassis, reliability));*/
        
    }

    
}
