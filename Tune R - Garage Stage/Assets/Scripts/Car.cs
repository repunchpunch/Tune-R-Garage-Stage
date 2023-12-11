using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    private float power;
    private double chassis;
    private double reliability;

    public Car ( 
                float power,
                double chassis,
                double reliability
               )
    {
        this.power = power;
        this.chassis = chassis;
        this.reliability = reliability;
    }
}
