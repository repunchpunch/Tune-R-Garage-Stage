using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CarBuilder : MonoBehaviour
{
    private Engine engine;
    private Turbo turbo;
    private Transmission transmission;
    private Suspension suspension;
    private Tires tires;
    private Brakes brakes;

    public CarBuilder ( 
                        Engine engine,
                        Turbo turbo,
                        Transmission transmission,
                        Suspension suspension,
                        Tires tires,
                        Brakes brakes
                      )
    {
        this.engine = engine;
        this.turbo = turbo;
        this.transmission = transmission;
        this.suspension = suspension;
        this.tires = tires;
        this.brakes = brakes;
    }

    private float CalculatePower()
    {
        return
                engine.getCurrentEnginePower()
              * (1 + turbo.getCurrentBoostInPercents()/100f)
              * (transmission.efficiencyInPercents/100f);
    }

    private double CalculateChassis()
    {
        return
                suspension.efficiencyInPercents
              * tires.efficiencyInPercents
              * brakes.efficiencyInPercents;
    }

    private double CalculateReliability()
    {
        float _p = CalculatePower();
        return
                engine.BreakEventEquation(_p)
              * transmission.BreakEventEquation(_p)
              * turbo.BreakEventEquation(_p)
              * suspension.BreakEventEquation(_p)
              * tires.BreakEventEquation(_p)
              * brakes.BreakEventEquation(_p);
    }

    public Car BuildCar()
    {
        return new Car(CalculatePower(), CalculateChassis(), CalculateReliability());
    }
}
