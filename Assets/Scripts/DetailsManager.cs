using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsManager : MonoBehaviour
{
    [SerializeField] const float CHAIN_PRICE = 532f;
    [SerializeField] const float WD_40 = 150f;
    // 1.68 uah for kwatt per hour
    [SerializeField] const float kWattPerHour = 1.68f;

    DBManager dBManager;
    // Start is called before the first frame update
    void Awake()
    {
        dBManager = GetComponent<DBManager>();
        
    }

    public Maintenance CalculateMaintenance(Vehicle vehicle)
    {
        Details details;
        string date = DateTime.Now.ToString("d");
        PersistentData.instance.vehicleDetails.TryGetValue(vehicle.vehicleID, out details);
        int clientDistance = UnityEngine.Random.Range(5, details.maxDistance);
        Debug.Log(clientDistance + " client distance");
        float priceForCharging = (clientDistance/details.averageSpeed) * details.batteryWatt * kWattPerHour;
        Debug.Log(priceForCharging + " PRICE for charging");
        // calculate mech service price
        int mechServiceCost = CalculateMechService(vehicle);
        Maintenance resultMaintenance = new Maintenance(date, priceForCharging, mechServiceCost, vehicle.vehicleID,
        clientDistance);
        return resultMaintenance;
    }

    private int CalculateMechService(Vehicle vehicle)
    {
        if(vehicle.vehicleID == 0)
        {
            return 10;
        }
        return 5;
    }
}
