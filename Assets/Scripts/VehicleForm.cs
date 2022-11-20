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

    Dictionary<int, Vehicle> vehiclesInfo;
    public int? vehicleId { get; private set; } = null;
    private DBManager dBManager;

    private void Awake()
    {
        dBManager = GetComponent<DBManager>();
        chosenVehicleText.text = "";
    }
    private void Start() {
        dBManager.FillListWithData(out vehiclesInfo);
        SpawnPanels();
    }
    public void SpawnPanels()
    {

        if (vehiclesInfo == null) return;
        foreach (var item in vehiclesInfo)
        {

            GameObject spawnedObject = Instantiate(panel, parent.transform);
            Debug.Log(item.Value.amount + " VEHICLE AMOUNT ");
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
        vehiclesInfo.TryGetValue(vehicleId, out vehicle);
        chosenVehicleText.text = $"You want to rent: {vehicle.vehicleName} ";
    }
    public int CalculateFullPrice(int hours)
    {
        Vehicle vehicle;
        vehiclesInfo.TryGetValue(this.vehicleId.Value, out vehicle);
        return (int)(hours * vehicle.pricePerHour);
    }
    public void DecreaseVehicleAmount()
    {
        Vehicle vehicle;
        vehiclesInfo.TryGetValue(this.vehicleId.Value, out vehicle);
        vehicle.DecreaseVehicleAmount();
        dBManager.UpdateVehicleAmount(vehicle);
        
        Debug.Log(vehicle.amount);
        StartCoroutine(ReloadVehicleForm());
    }
    private IEnumerator ReloadVehicleForm()
    {
        ClearPanels();
        yield return null;
        dBManager.FillListWithData(out vehiclesInfo);
        yield return null;
        SpawnPanels();
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
