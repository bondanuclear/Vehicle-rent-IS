using System.Collections;
using System.Collections.Generic;


public class Details 
{
    public int detailsID {get; private set;}
    public int vehicleID {get; private set;}
    public int maxDistance {get; private set;}
    public int batteryWatt {get; private set;}
    public float averageSpeed {get; private set;}
    public float hoursToCharge {get; private set;}
    public Details(int detailsID, int vehicleID, int maxDistance, int batteryWatt, float averageSpeed, float hoursToCharge )
    {
        this.detailsID = detailsID;
        this.vehicleID = vehicleID;
        this.maxDistance = maxDistance;
        this.batteryWatt = batteryWatt;
        this.averageSpeed = averageSpeed;
        this.hoursToCharge = hoursToCharge;
    }
}
