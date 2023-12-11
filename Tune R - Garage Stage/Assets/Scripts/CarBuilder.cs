using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class CarBuilder : MonoBehaviour
{
    private Dictionary<System.Type, System.Action<InventoryItem>> itemActions;

    void Awake()
    {
        itemActions = new Dictionary<System.Type, System.Action<InventoryItem>>
        {
            { typeof(Engine),       item => { engine       = (Engine)item.myItem;       } },
            { typeof(Turbo),        item => { turbo        = (Turbo)item.myItem;        } },
            { typeof(Transmission), item => { transmission = (Transmission)item.myItem; } },
            { typeof(Suspension),   item => { suspension   = (Suspension)item.myItem;   } },
            { typeof(Tires),        item => { tires        = (Tires)item.myItem;        } },
            { typeof(Brakes),       item => { brakes       = (Brakes)item.myItem;       } }
        };
    }

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

    void OnEnable()
    {
        InventorySlot.OnItemAdded += UpdateCarPart;
    }

    void OnDisable()
    {
        InventorySlot.OnItemAdded -= UpdateCarPart;
    }

    void UpdateCarPart(InventoryItem item)
    {
        System.Type itemType = item.myItem.GetType();
        if (itemActions.ContainsKey(itemType))
        {
            itemActions[itemType](item);
        }

        BuildCar();
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
                suspension.efficiencyInPercents/100f
              * tires.efficiencyInPercents/100f
              * brakes.efficiencyInPercents/100f;
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
        Car car = ScriptableObject.CreateInstance<Car>();
        if (!AreAllPartsPresent()) car.Initialize(0, 0, 0);
        else car.Initialize(CalculatePower(), CalculateChassis(), CalculateReliability());
        return car;

    }

    private bool AreAllPartsPresent()
    {
        List<Item> parts = new List<Item> { engine, turbo, transmission, suspension, tires, brakes };

        foreach (var part in parts)
        {
            if (part == null) return false;
        }

        return true;
    }
}
