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
    int? clientIDHelper = null;
    private void Start() {
        dBManager = FindObjectOfType<DBManager>();
        
    }
    private void OnEnable() {
        
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
    }
    public void DeleteClient()
    {
        if(clientIDHelper == null)
        {
            Debug.LogWarning("You have not chosen a client to delete");
            return;
        }
        

        PersistentData.instance.DeleteClientFromDictionary(clientIDHelper.Value);
        Transform objectToDelete = parentObject.gameObject.transform.Find(clientIDHelper.Value+"Client");
        //Debug.Log($"Gonna destroy {(clientIDHelper - 1).Value} object");
        Destroy(objectToDelete.gameObject);
    }
    // можливо колись знадобиться
    private void AddClientPanel()
    {
        Client client = dBManager.GetLastRow();
    }
    
}
