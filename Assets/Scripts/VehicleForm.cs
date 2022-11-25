using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleForm : MonoBehaviour
{
    [Header("Vehicle Panel")]
    [SerializeField] GameObject panel = null;
    public GameObject parent  = null;
    [SerializeField] TextMeshProUGUI chosenVehicleText = null;

    //Dictionary<int, Vehicle> vehiclesInfo;
    public int? vehicleId { get; private set; } = null;
    private DBManager dBManager;
    public bool haveChosen {get; set;} = false;
    private void Awake()
    {
        dBManager = GetComponent<DBManager>();
        chosenVehicleText.text = "";
    }
    private void Start() {
        //dBManager.FillListWithData(out PersistentData.instance.vehiclesInfo);
        SpawnVehiclePanels();
    }
    public void SpawnVehiclePanels()
    {

        if (PersistentData.instance.vehiclesInfo == null) return;
        foreach (var item in PersistentData.instance.vehiclesInfo)
        {

            GameObject spawnedObject = Instantiate(panel, parent.transform);
            //Debug.Log(item.Value.amount + " VEHICLE AMOUNT ");
            if(item.Value.amount <= 0) 
            {
                spawnedObject.GetComponent<Button>().interactable = false;
            } else spawnedObject.GetComponent<Button>().interactable = true;
            spawnedObject.name = item.Key.ToString() + "Vehicle";
            spawnedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Value.vehicleName;
            spawnedObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Value.pricePerHour.ToString() + " UAH/HOUR";
            spawnedObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.Value.type;
            spawnedObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = item.Value.amount.ToString();
            // Debug.Log(spawnedObject.name);
            // Debug.Log(item.Key + " VEHICLE ID ");
            spawnedObject.GetComponent<Button>().onClick.AddListener(delegate { ChooseVehicle(item.Key); });
        }
    }
    public void ChooseVehicle(int vehicleId)
    {
        Debug.Log("Button clicked = " + vehicleId);
        this.vehicleId = vehicleId;
        Debug.Log(this.vehicleId);
        Vehicle vehicle;
        PersistentData.instance.vehiclesInfo.TryGetValue(vehicleId, out vehicle);
        chosenVehicleText.text = $"You want to rent: {vehicle.vehicleName} ";
        haveChosen = true;
    }
    public int CalculateFullPrice(int hours)
    {
        Vehicle vehicle;
        PersistentData.instance.vehiclesInfo.TryGetValue(this.vehicleId.Value, out vehicle);
        return (int)(hours * vehicle.pricePerHour);
    }
    //
    //
    // needs refactoring
    public void DecreaseVehicleAmount()
    {
        
        Vehicle vehicle;
        PersistentData.instance.vehiclesInfo.TryGetValue(this.vehicleId.Value, out vehicle);
        int finalAmount = vehicle.DecreaseVehicleAmount();
        Debug.Log(finalAmount + " FINAL AMOUNT ");
        Vehicle updatedVehicle = new Vehicle(vehicle.vehicleID, vehicle.vehicleName,
        vehicle.pricePerHour, vehicle.type, vehicle.totalMileage, finalAmount);
        // vehicle.DecreaseAmount();
        dBManager.UpdateVehicleAmount(vehicle);
        PersistentData.instance.vehiclesInfo[vehicle.vehicleID] = updatedVehicle;
        ChangeVehiclePanel(updatedVehicle);
        //Debug.Log(vehicle.amount);
        //StartCoroutine(ReloadVehicleForm());
    }

    private void ChangeVehiclePanel(Vehicle vehicle)
    {
        GameObject objectToChange = parent.transform.Find(vehicle.vehicleID + "Vehicle").gameObject;
        objectToChange.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = vehicle.amount.ToString(); 
    }

    private IEnumerator ReloadVehicleForm()
    {
        ClearPanels();
        yield return null;
        dBManager.FillListWithData(out PersistentData.instance.vehiclesInfo);
        yield return null;
        SpawnVehiclePanels();
    }
    private void ClearPanels()
    {
        foreach(Transform item in parent.transform)
        {
            Destroy(item.gameObject);
        }
    }
    // public void Return()
    // {
    //     this.vehicleId = null;
    //     chosenVehicleText.text = "";
    // }
}
