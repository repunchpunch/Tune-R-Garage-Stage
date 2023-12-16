using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class CarBuilder : MonoBehaviour
{
    public Bar PowerBar;
    public Bar ChassisBar;
    public Bar ReliabilityBar;

    private Dictionary<System.Type, System.Action<InventoryItem>> addActions;
    private Dictionary<SlotTag, System.Action> removeActions;

    public static CarBuilder Instance;

    void Awake()
    {
        Instance = this;

        addActions = new Dictionary<System.Type, System.Action<InventoryItem>>
        {
            { typeof(Engine),       item => { engine       = (Engine)item.myItem;       } },
            { typeof(Turbo),        item => { turbo        = (Turbo)item.myItem;        } },
            { typeof(Transmission), item => { transmission = (Transmission)item.myItem; } },
            { typeof(Suspension),   item => { suspension   = (Suspension)item.myItem;   } },
            { typeof(Tires),        item => { tires        = (Tires)item.myItem;        } },
            { typeof(Brakes),       item => { brakes       = (Brakes)item.myItem;       } }
        };

        removeActions = new Dictionary<SlotTag, System.Action>
        {
            { SlotTag.engine,       () => { engine       = null; } },
            { SlotTag.turbo,        () => { turbo        = null; } },
            { SlotTag.transmission, () => { transmission = null; } },
            { SlotTag.suspension,   () => { suspension   = null; } },
            { SlotTag.tires,        () => { tires        = null; } },
            { SlotTag.brakes,       () => { brakes       = null; } }
        };
    }

    private Engine engine;
    private Turbo turbo;
    private Transmission transmission;
    private Suspension suspension;
    private Tires tires;
    private Brakes brakes;
    public CarBody carBody;

    public CarBuilder ( 
                        Engine engine,
                        Turbo turbo,
                        Transmission transmission,
                        Suspension suspension,
                        Tires tires,
                        Brakes brakes,
                        CarBody carBody
                      )
    {
        this.engine = engine;
        this.turbo = turbo;
        this.transmission = transmission;
        this.suspension = suspension;
        this.tires = tires;
        this.brakes = brakes;
        this.carBody = carBody;
    }

    void OnEnable()
    {
        InventorySlot.OnItemAdded += AddPartAndBuild;
    }

    void OnDisable()
    {
        InventorySlot.OnItemAdded -= AddPartAndBuild;
    }

    void AddPartAndBuild(InventoryItem item)
    {
        System.Type itemType = item.myItem.GetType();
        if (addActions.ContainsKey(itemType))
        {
            addActions[itemType](item);
        }

        BuildCarAndUpdateBars();
    }

    public void RemovePartAndBuild(SlotTag tag)
    {
        if (removeActions.ContainsKey(tag))
        {
            removeActions[tag]();
        }

        BuildCarAndUpdateBars();
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
                1 -
                engine.BreakEventEquation(_p)
              * transmission.BreakEventEquation(_p)
              * turbo.BreakEventEquation(_p)
              * suspension.BreakEventEquation(_p)
              * tires.BreakEventEquation(_p)
              * brakes.BreakEventEquation(_p);
    }

    public Car BuildCarAndUpdateBars()
    {
        Car car = ScriptableObject.CreateInstance<Car>();
        if (!AreAllPartsPresent()) car.Initialize(0, 0, 0);
        else 
        {
            float _power = CalculatePower();
            double _chassis = CalculateChassis();
            double _reliability = CalculateReliability();
            car.Initialize(_power, _chassis, _reliability);
            PowerBar.updateBar(_power);
            ChassisBar.updateBar((float)_chassis);
            ReliabilityBar.updateBar((float)_reliability);
        }
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
