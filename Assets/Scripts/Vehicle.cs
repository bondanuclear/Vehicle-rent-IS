using System.Collections;
using System.Collections.Generic;


public class Vehicle 
{
    public int vehicleID {get; private set;}
    public string vehicleName {get; private set;}
    public float pricePerHour {get; private set;}
    public string type { get; private set; }
    public float maxSpeed {get; private set;}
    public int amount { get; private set; }
    public Vehicle(int vehicleID, string vehicleName, float pricePerHour, string type, float maxSpeed, int amount)
    {
        this.vehicleID = vehicleID;
        this.vehicleName = vehicleName;
        this.pricePerHour = pricePerHour;
        this.type = type;
        this.amount = amount;
        this.maxSpeed = maxSpeed;
    }

}
