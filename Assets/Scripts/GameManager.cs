using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
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
// to do: if a person puts the same number inside the input field: track the exception
// and alert the user.
// to do: add "Amount" of vehicles to UI. Amount -= 1 after person rents it.
// to do: if the amount of a certain vehicle is <= 0 then disable the ability to rent it.
// if a person returns a vehicle then amount of this vehicles += 1
// to do: manager scene, ability to track the statistics of rented vehicles
// to do: new table with income (рухи в лекціях): data vehicle hours fullPrice 
// conclusion: month vehicle allHours sumPrice - conclusion after each month
// 
// update DBManager : ability to update fields. ability to find by id or sth else;
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
