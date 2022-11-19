using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // [Header("Vehicle Panel")]
    // [SerializeField] GameObject panel = null;
    // [SerializeField] GameObject parent = null;
    // [SerializeField] TextMeshProUGUI chosenVehicleText = null;
    
    // Dictionary<int, Vehicle> vehiclesInfo;
    // public int? vehicleId {get; private set;} = null;
    [SerializeField] Button choose;
    [SerializeField] Button returnToVehicles;
    DBManager dBManager;
    RegisterForm registerForm;
    VehicleForm vehicleForm;
    void Awake()
    {
        //dBManager = GetComponent<DBManager>();
        vehicleForm = GetComponent<VehicleForm>();
        registerForm = GetComponent<RegisterForm>();
    }
    private void OnEnable() {
        
        choose.onClick.AddListener(ShowRegistrationForm);
        returnToVehicles.onClick.AddListener(CloseRegistration);
    } 
    public void ShowRegistrationForm()
    {
        bool shouldHide = registerForm.ActivateRegForm();
        choose.gameObject.SetActive(!shouldHide);
    }
    public void CloseRegistration()
    {
        registerForm.DeactivateRegForm();
        choose.gameObject.SetActive(true);
    }
// to do: if a person
    // public void AddUser()
    // {
    //     Client client = new Client(5, "aaa", "ddd", "178489", 430);
    //     dBManager.AddUserToTable(client);
    // }
    // Update is called once per frame
    // void SpawnPanels()
    // {
        
    //     if(vehiclesInfo == null) return;

    //     foreach(var item in vehiclesInfo)
    //     {
            
    //         GameObject spawnedObject  = Instantiate(panel, parent.transform);
    //         spawnedObject.name = item.Key.ToString() + "Vehicle";
    //         spawnedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Value.vehicleName;
    //         spawnedObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Value.pricePerHour.ToString() + " UAH/HOUR";
    //         spawnedObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.Value.type;
    //         // Debug.Log(spawnedObject.name);
    //         // Debug.Log(item.Key + " VEHICLE ID ");
    //         spawnedObject.GetComponent<Button>().onClick.AddListener(delegate{ChooseVehicle(item.Key);});
            
            
    //     }
    // }
    // public void ChooseVehicle(int vehicleId)
    // {
    //     //Debug.Log("Button clicked = " + vehicleId);
    //     //Debug.Log(this.vehicleId);
    //     this.vehicleId = vehicleId;
    //     Vehicle vehicle;
    //     vehiclesInfo.TryGetValue(this.vehicleId.Value, out vehicle);
    //     chosenVehicleText.text = $" {vehicle.vehicleName} ";
    // }
    // public void Return()
    // {
    //     this.vehicleId = null;
    //     chosenVehicleText.text = "";
    // }
}
