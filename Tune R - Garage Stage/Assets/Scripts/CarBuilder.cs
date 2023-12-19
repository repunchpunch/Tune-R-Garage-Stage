using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
            { typeof(Engine),       item => { engineInventoryItem       = item; engine = item.myItem as Engine;             } },
            { typeof(Turbo),        item => { turboInventoryItem        = item; turbo = item.myItem as Turbo;               } },
            { typeof(Transmission), item => { transmissionInventoryItem = item; transmission = item.myItem as Transmission; } },
            { typeof(Suspension),   item => { suspensionInventoryItem   = item; suspension = item.myItem as Suspension;     } },
            { typeof(Tires),        item => { tiresInventoryItem        = item; tires = item.myItem as Tires;               } },
            { typeof(Brakes),       item => { brakesInventoryItem       = item; brakes = item.myItem as Brakes;             } }
        };

        removeActions = new Dictionary<SlotTag, System.Action>
        {
            { SlotTag.engine,       () => { engineInventoryItem       = null; engine = null;       } },
            { SlotTag.turbo,        () => { turboInventoryItem        = null; turbo = null;        } },
            { SlotTag.transmission, () => { transmissionInventoryItem = null; transmission = null; } },
            { SlotTag.suspension,   () => { suspensionInventoryItem   = null; suspension = null;   } },
            { SlotTag.tires,        () => { tiresInventoryItem        = null; tires = null;        } },
            { SlotTag.brakes,       () => { brakesInventoryItem       = null; brakes = null;       } }
        };
    }

    private InventoryItem engineInventoryItem;
    private InventoryItem turboInventoryItem;
    private InventoryItem transmissionInventoryItem;
    private InventoryItem suspensionInventoryItem;
    private InventoryItem tiresInventoryItem;
    private InventoryItem brakesInventoryItem;

    public CarBody carBody;

    Engine engine;
    Turbo turbo;
    Transmission transmission;
    Suspension suspension;
    Tires tires;
    Brakes brakes;

    // public CarBuilder ( 
    //                     Engine engine,
    //                     Turbo turbo,
    //                     Transmission transmission,
    //                     Suspension suspension,
    //                     Tires tires,
    //                     Brakes brakes,
    //                     CarBody carBody
    //                   )
    // {
    //     this.engine = engine;
    //     this.turbo = turbo;
    //     this.transmission = transmission;
    //     this.suspension = suspension;
    //     this.tires = tires;
    //     this.brakes = brakes;
    //     this.carBody = carBody;
    // }

    void OnEnable()
    {
        InventorySlot.OnItemAdded += AddPartAndBuild;
        Inventory.OnRacingDamage += UpdateBars;
    }

    void OnDisable()
    {
        InventorySlot.OnItemAdded -= AddPartAndBuild;
        Inventory.OnRacingDamage -= UpdateBars;
    }

    void AddPartAndBuild(InventoryItem item)
    {
        System.Type itemType = item.myItem.GetType();
        if (addActions.ContainsKey(itemType))
        {
            Debug.Log("Added");
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

    public float CalculatePower()
    {
        return
                //engine.getCurrentEnginePower()
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
        float _rel =
            1 -
                engineInventoryItem.durability.BreakEventEquation(_p)
              * transmissionInventoryItem.durability.BreakEventEquation(_p)
              * turboInventoryItem.durability.BreakEventEquation(_p)
              * suspensionInventoryItem.durability.BreakEventEquation(_p)
              * tiresInventoryItem.durability.BreakEventEquation(_p)
              * brakesInventoryItem.durability.BreakEventEquation(_p);
        Debug.Log(_rel);
        return _rel;
    }

    public Car BuildCarAndUpdateBars()
    {
        float _power;
        double _chassis;
        double _reliability;
        Car car = ScriptableObject.CreateInstance<Car>();
        if (!AreAllPartsPresent())
        {
            _power = 0;
            _chassis = 0;
            _reliability = 0;
        }
        else 
        {
            _power = CalculatePower();
            _chassis = CalculateChassis();
            _reliability = CalculateReliability();
        }
        car.Initialize(_power, _chassis, _reliability);
        PowerBar.updateBar(_power);
        ChassisBar.updateBar((float)_chassis);
        ReliabilityBar.updateBar((float)_reliability);
        return car;

    }

    public void UpdateBars()
    {
        float _power;
        double _chassis;
        double _reliability;
        if (!AreAllPartsPresent())
        {
            _power = 0;
            _chassis = 0;
            _reliability = 0;
        }
        else
        {
            _power = CalculatePower();
            _chassis = CalculateChassis();
            _reliability = CalculateReliability();
        }
        PowerBar.updateBar(_power);
        ChassisBar.updateBar((float)_chassis);
        ReliabilityBar.updateBar((float)_reliability);
    }

    public bool AreAllPartsPresent()
    {
        List<Item> parts = new List<Item> { engine, turbo, transmission, suspension, tires, brakes };

        foreach (var part in parts)
        {
            if (part == null) return false;
        }

        return true;
    }
}
