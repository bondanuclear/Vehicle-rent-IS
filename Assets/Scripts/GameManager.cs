using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    DBManager dBManager;
    [Header("Vehicle Panel")]
    [SerializeField] GameObject panel = null;
    [SerializeField] GameObject parent = null;
    Dictionary<int, Vehicle> vehiclesInfo;
    int? vehicleId = null;
    [SerializeField] TextMeshProUGUI chosenVehicleText = null;
    // Start is called before the first frame update
    void Awake()
    {
        dBManager = GetComponent<DBManager>();
    }
    private void Start() {
        
        dBManager.FillListWithData(out vehiclesInfo);
        Debug.Log(vehiclesInfo.Count);
        SpawnPanels();
        chosenVehicleText.text = "";
    } 
    // public void AddUser()
    // {
    //     Client client = new Client(5, "aaa", "ddd", "178489", 430);
    //     dBManager.AddUserToTable(client);
    // }
    // Update is called once per frame
    void SpawnPanels()
    {
        
        if(vehiclesInfo == null) return;

        foreach(var item in vehiclesInfo)
        {
            
            GameObject spawnedObject  = Instantiate(panel, parent.transform);
            spawnedObject.name = item.Key.ToString() + "Vehicle";
            spawnedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Value.vehicleName;
            spawnedObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Value.pricePerHour.ToString() + " UAH/HOUR";
            spawnedObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.Value.type;
            // Debug.Log(spawnedObject.name);
            // Debug.Log(item.Key + " VEHICLE ID ");
            spawnedObject.GetComponent<Button>().onClick.AddListener(delegate{ChooseVehicle(item.Key);});
            
            
        }
    }
    public void ChooseVehicle(int vehicleId)
    {
        Debug.Log("Button clicked = " + vehicleId);
        this.vehicleId = vehicleId;
        Debug.Log(this.vehicleId);
        Vehicle vehicle;
        vehiclesInfo.TryGetValue(vehicleId, out vehicle);
        chosenVehicleText.text = $" {vehicle.vehicleName} ";
    }
    public void Return()
    {
        this.vehicleId = null;
        chosenVehicleText.text = "";
    }
}
