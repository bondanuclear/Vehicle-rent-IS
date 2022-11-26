using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maintenance 
{
    public int maintenanceID {get; private set;}
    public string date {get; private set;}
    public float powerChargeCost {get; private set;}
    public float mechServiceCost {get; private set;}
    public int vehicleID {get; private set;}
    public int mileage {get; private set;}
    public Maintenance(string date, float powerChargeCost,
     float mechServiceCost, int vehicleID, int mileage, int maintenanceID = 0)
    {
        this.maintenanceID = maintenanceID;
        this.date = date;
        this.powerChargeCost = powerChargeCost;
        this.mechServiceCost = mechServiceCost;
        this.mileage = mileage;
        this.vehicleID = vehicleID;
    }
}
