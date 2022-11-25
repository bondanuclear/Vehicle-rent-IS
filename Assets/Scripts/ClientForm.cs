using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientForm : MonoBehaviour
{
    [SerializeField] GameObject clientPanel;
    [SerializeField] GameObject parentObject;
    bool hasSpawned = false;
    DBManager dBManager;
    UserInfo userInfo;
    public int? clientIDHelper {get; private set;} = null;
    private void Awake() {
        userInfo = GetComponent<UserInfo>();
    }
    private void Start() {
        dBManager = FindObjectOfType<DBManager>();
        
    }
    public void SpawnClientPanels()
    {
        if(hasSpawned) return;
        if (PersistentData.instance.clientsInfo == null) return;
        hasSpawned = true;
        foreach(var item in PersistentData.instance.clientsInfo)
        {
            GameObject spawnedObject = Instantiate(clientPanel, parentObject.transform);
            
            spawnedObject.name = item.Key.ToString() + "Client";
            spawnedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Value.firstName;
            spawnedObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Value.surname;
            spawnedObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.Value.phoneNumber;
            spawnedObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = item.Value.rentedHours.ToString();
            Vehicle vehicle;
            PersistentData.instance.vehiclesInfo.TryGetValue(item.Value.VehicleID, out vehicle);
            spawnedObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = vehicle.vehicleName;
            // Debug.Log(spawnedObject.name);
            // Debug.Log(item.Key + " VEHICLE ID ");
            spawnedObject.GetComponent<Button>().onClick.AddListener(delegate { ChooseClient(item.Key); });
            
        }
    }
    public void ChooseClient(int clientId)
    {
       Debug.Log("Client ID = " + clientId);
       clientIDHelper = clientId;
       StartCoroutine(userInfo.ShowUserInfo(clientId));

    }
    // розрахувати вартість
    public void EstimateValue()
    {

    }
    public void DeleteClient()
    {
        if(clientIDHelper == null)
        {
            Debug.LogWarning("You have not chosen a client to delete");
            return;
        }
        // оскільки ми розраховуємо клієнта, нам потрібно повернути транспорт, який
        // він брав
        

        Vehicle vehicle = GetVehicleByClientID(this.clientIDHelper.Value);
        PersistentData.instance.DeleteClientFromDictionary(clientIDHelper.Value);
        Transform objectToDelete = parentObject.gameObject.transform.Find(clientIDHelper.Value+"Client");
        //Debug.Log($"Gonna destroy {(clientIDHelper - 1).Value} object");
        Destroy(objectToDelete.gameObject);
        clientIDHelper = null;
        ReturnTakenTransport(vehicle);
    }
    private Vehicle GetVehicleByClientID(int clientIDHelper)
    {
        Vehicle result;
        Client client;
        PersistentData.instance.clientsInfo.TryGetValue(clientIDHelper, out client);
        PersistentData.instance.vehiclesInfo.TryGetValue(client.VehicleID, out result);
        return result;
    }
    public void ReturnTakenTransport(Vehicle vehicle)
    {
        // maybe needs refactoring, but at least it works for now
        int finalAmount = vehicle.IncreaseVehicleAmount();
        Debug.Log(finalAmount + " FINAL AMOUNT ");
        Vehicle updatedVehicle = new Vehicle(vehicle.vehicleID, vehicle.vehicleName, 
        vehicle.pricePerHour, vehicle.type, vehicle.totalMileage, finalAmount);
        dBManager.UpdateVehicleAmount(updatedVehicle);
        PersistentData.instance.vehiclesInfo[vehicle.vehicleID] = updatedVehicle;
        Debug.Log(PersistentData.instance.vehiclesInfo[vehicle.vehicleID].amount + " UPDATED ");
    }
    // можливо колись знадобиться
    private void AddClientPanel()
    {
        Client client = dBManager.GetLastRow();
    }
    
}
