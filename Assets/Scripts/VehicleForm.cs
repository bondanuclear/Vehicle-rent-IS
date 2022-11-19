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
            spawnedObject.name = item.Key.ToString() + "Vehicle";
            spawnedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Value.vehicleName;
            spawnedObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Value.pricePerHour.ToString() + " UAH/HOUR";
            spawnedObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.Value.type;
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
    // public void Return()
    // {
    //     this.vehicleId = null;
    //     chosenVehicleText.text = "";
    // }
}
