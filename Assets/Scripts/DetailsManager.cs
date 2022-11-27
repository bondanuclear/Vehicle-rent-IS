using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsManager : MonoBehaviour
{
    // bicycle
    [SerializeField] const float CHAIN_PRICE = 532f;
    [SerializeField] const float WD_40 = 150f;
    // monowheel
    [SerializeField] const float MONOWHEEL_TIRE = 560f;
    [SerializeField] const float MUDGUARD = 328.99f;
    [SerializeField] const float MONOWHEEL_BATTERY = 3280f;
    // skate
    [SerializeField] const float SKATE_WHEELS = 4*260f;
    // scooter
    [SerializeField] const float SCOOTER_WHEELS = 200*2f;
    // electro bicycle
    [SerializeField] const float EBICYCLE_BATTERY = 12000f;
    [SerializeField] const float EBICYCLE_WHEELS = 2*5000f;
    // ESCOOTER
    [SerializeField] const float ESCOOTER_WHEELS = 2*4000f;
    [SerializeField] const float ESCOOTER_BATTERY = 7000f;
    // 1.68 uah for kwatt per hour
    [SerializeField] const float kWattPerHour = 1.68f;

    DBManager dBManager;
    // Start is called before the first frame update
    void Awake()
    {
        dBManager = FindObjectOfType<DBManager>();
        Debug.Log(DateTime.Now.ToString("yyyy-MM-dd"));
    }

    public Maintenance CalculateMaintenance(Vehicle vehicle)
    {
        Details details;
        string date = Maintenance.InvertDate(DateTime.Today);
        PersistentData.instance.vehicleDetails.TryGetValue(vehicle.vehicleID, out details);
        int clientDistance = UnityEngine.Random.Range(5, details.maxDistance);
        //Debug.Log(clientDistance + " client distance");
        //Debug.Log(details.averageSpeed + " average speed" + details.batteryWatt / 1000);
        float priceForCharging = ((clientDistance/details.averageSpeed) * (details.batteryWatt) * kWattPerHour) / 1000;
        //Debug.Log(priceForCharging + " PRICE for charging");
        // calculate mech service price
        float mechServiceCost = CalculateMechService(vehicle, clientDistance);
        Maintenance resultMaintenance = new Maintenance(date, priceForCharging, mechServiceCost, vehicle.vehicleID,
        clientDistance);
        return resultMaintenance;
    }
    // temporary - needs refactoring
    // maybe create different classes for each vehicle and use interfaces
    // mb new table VehicleMileageControl
    private float CalculateMechService(Vehicle vehicle, int clientDistance)
    {
        if(vehicle.vehicleID == 0)
        {
            float sum = 0;
            //500
            if(vehicle.totalMileage > 500)
            {
                Vehicle newMileageVehicle = new Vehicle(vehicle.vehicleID, vehicle.vehicleName,
            vehicle.pricePerHour, vehicle.type, 0, vehicle.amount);
                PersistentData.instance.vehiclesInfo[vehicle.vehicleID] = newMileageVehicle;
                dBManager.UpdateVehicleMileage(newMileageVehicle);
                sum += MONOWHEEL_BATTERY;
                sum += MONOWHEEL_TIRE;
                sum += MUDGUARD;
            }
            else
            {
                float mileage = clientDistance + vehicle.totalMileage;
                Vehicle newMileageVehicle = new Vehicle(vehicle.vehicleID, vehicle.vehicleName,
            vehicle.pricePerHour, vehicle.type, mileage, vehicle.amount);
                PersistentData.instance.vehiclesInfo[vehicle.vehicleID] = newMileageVehicle;
                dBManager.UpdateVehicleMileage(newMileageVehicle);
            }
            sum += 1.5f;
            return sum;
        }
        else if(vehicle.vehicleID == 1)
        {
            float sum = 0;

            if(vehicle.totalMileage > 750)
            {
                Vehicle newMileageVehicle = new Vehicle(vehicle.vehicleID, vehicle.vehicleName,
            vehicle.pricePerHour, vehicle.type, 0, vehicle.amount);
                PersistentData.instance.vehiclesInfo[vehicle.vehicleID] = newMileageVehicle;
                dBManager.UpdateVehicleMileage(newMileageVehicle);
                sum += CHAIN_PRICE;
            }
            else
            {
                float mileage = clientDistance + vehicle.totalMileage;
                Vehicle newMileageVehicle = new Vehicle(vehicle.vehicleID, vehicle.vehicleName,
            vehicle.pricePerHour, vehicle.type, mileage, vehicle.amount);
                PersistentData.instance.vehiclesInfo[vehicle.vehicleID] = newMileageVehicle;
                dBManager.UpdateVehicleMileage(newMileageVehicle);
            }
            // cleaning of bicycle chain
            sum += 1.5f;
            return sum;
        }
        else if(vehicle.vehicleID == 3)
        {
            float sum = 0;

            if (vehicle.totalMileage > 700)
            {
                Vehicle newMileageVehicle = new Vehicle(vehicle.vehicleID, vehicle.vehicleName,
            vehicle.pricePerHour, vehicle.type, 0, vehicle.amount);
                PersistentData.instance.vehiclesInfo[vehicle.vehicleID] = newMileageVehicle;
                dBManager.UpdateVehicleMileage(newMileageVehicle);
                sum += SKATE_WHEELS;
            }
            else
            {
                float mileage = clientDistance + vehicle.totalMileage;
                Vehicle newMileageVehicle = new Vehicle(vehicle.vehicleID, vehicle.vehicleName,
            vehicle.pricePerHour, vehicle.type, mileage, vehicle.amount);
                PersistentData.instance.vehiclesInfo[vehicle.vehicleID] = newMileageVehicle;
                dBManager.UpdateVehicleMileage(newMileageVehicle);
            }   
            // cleaning of bicycle chain
            sum += 1.5f;
            return sum;
        }   else if (vehicle.vehicleID == 23)
        {
            float sum = 0;
            //500
            if (vehicle.totalMileage > 600)
            {
                Vehicle newMileageVehicle = new Vehicle(vehicle.vehicleID, vehicle.vehicleName,
            vehicle.pricePerHour, vehicle.type, 0, vehicle.amount);
                PersistentData.instance.vehiclesInfo[vehicle.vehicleID] = newMileageVehicle;
                dBManager.UpdateVehicleMileage(newMileageVehicle);
                sum += SCOOTER_WHEELS;          
            }
            else
            {
                float mileage = clientDistance + vehicle.totalMileage;
                Vehicle newMileageVehicle = new Vehicle(vehicle.vehicleID, vehicle.vehicleName,
            vehicle.pricePerHour, vehicle.type, mileage, vehicle.amount);
                PersistentData.instance.vehiclesInfo[vehicle.vehicleID] = newMileageVehicle;
                dBManager.UpdateVehicleMileage(newMileageVehicle);
            }
            sum += 1.5f;
            return sum;
        }
        else if (vehicle.vehicleID == 22)
        {
            float sum = 0;
            //500
            if (vehicle.totalMileage > 2000)
            {
                Vehicle newMileageVehicle = new Vehicle(vehicle.vehicleID, vehicle.vehicleName,
            vehicle.pricePerHour, vehicle.type, 0, vehicle.amount);
                PersistentData.instance.vehiclesInfo[vehicle.vehicleID] = newMileageVehicle;
                dBManager.UpdateVehicleMileage(newMileageVehicle);
                sum += EBICYCLE_WHEELS;
                sum += EBICYCLE_BATTERY;
            }
            else
            {
                float mileage = clientDistance + vehicle.totalMileage;
                Vehicle newMileageVehicle = new Vehicle(vehicle.vehicleID, vehicle.vehicleName,
            vehicle.pricePerHour, vehicle.type, mileage, vehicle.amount);
                PersistentData.instance.vehiclesInfo[vehicle.vehicleID] = newMileageVehicle;
                dBManager.UpdateVehicleMileage(newMileageVehicle);
            }
            sum += 1.5f;
            return sum;
        }
        else if (vehicle.vehicleID == 24 || vehicle.vehicleID == 25)
        {
            float sum = 0;
            //500
            if (vehicle.totalMileage > 2000)
            {
                Vehicle newMileageVehicle = new Vehicle(vehicle.vehicleID, vehicle.vehicleName,
            vehicle.pricePerHour, vehicle.type, 0, vehicle.amount);
                PersistentData.instance.vehiclesInfo[vehicle.vehicleID] = newMileageVehicle;
                dBManager.UpdateVehicleMileage(newMileageVehicle);
                sum += ESCOOTER_BATTERY;
                sum += ESCOOTER_WHEELS;
            }
            else
            {
                float mileage = clientDistance + vehicle.totalMileage;
                Vehicle newMileageVehicle = new Vehicle(vehicle.vehicleID, vehicle.vehicleName,
            vehicle.pricePerHour, vehicle.type, mileage, vehicle.amount);
                PersistentData.instance.vehiclesInfo[vehicle.vehicleID] = newMileageVehicle;
                dBManager.UpdateVehicleMileage(newMileageVehicle);
            }
            sum += 1.5f;
            return sum;
        }
        return 0;
    }
}
