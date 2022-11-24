using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData instance;
    
    public Dictionary<int, Vehicle> vehiclesInfo;

    public Dictionary<int, Client> clientsInfo;
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
}
