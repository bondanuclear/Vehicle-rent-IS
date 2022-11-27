using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData instance;
    
    public Dictionary<int, Vehicle> vehiclesInfo;

    public Dictionary<int, Client> clientsInfo;
    // info about battery, average speed, charging hours. also contains vehicle id
    // for easy access
    public Dictionary<int, Details> vehicleDetails;
    // info about different types of income
    public Dictionary<int, Income> dailyIncome;

    public Dictionary<int, Income> dailyRelativeIncome;

    public Dictionary<int, Income> monthlyIncome;
    DBManager dBManager;
    private void OnEnable() {
        RegisterForm.addClient += AddClientToDictionary;
        
    }
    private void OnDisable() {
        RegisterForm.addClient -= AddClientToDictionary;
    }
    private void Awake() {
        if(instance != null)
        {
            Debug.Log("I already exist");
            return;   
        }
        else
        {
            instance = this;
            dBManager = GetComponent<DBManager>();
            dBManager.FillListWithData(out vehiclesInfo);
            dBManager.FillListWithClientsData(out clientsInfo);
            dBManager.FillDetailsListWithData(out vehicleDetails);
            dBManager.FillIncomeData(out dailyIncome);
            dBManager.FillRelativeIncomeData(out dailyRelativeIncome);
            dBManager.FillMonthlyIncomeData(out monthlyIncome);
            Debug.Log($"Filling data in {this.name} script");
            DontDestroyOnLoad(this.gameObject);
        }

    }
    private void AddClientToDictionary(Client client)
    {
        Debug.Log(clientsInfo.Count.ToString() + " COUNT CLIENTS " ?? "its null");
        Debug.Log("Client ID " + client.clientID);
        clientsInfo.Add(client.clientID, client);
    }
    public void DeleteClientFromDictionary(int clientID)
    {
        dBManager.DeleteClientFromTable(clientID);
        clientsInfo.Remove(clientID);
    }
    public int CalculateFullPrice(int vehicleID, int hours)
    {
        Vehicle vehicle;
        PersistentData.instance.vehiclesInfo.TryGetValue(vehicleID, out vehicle);
        return (int)(hours * vehicle.pricePerHour);
    }
}
