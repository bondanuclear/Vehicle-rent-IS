using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData instance;
    
    public Dictionary<int, Vehicle> vehiclesInfo;

    public Dictionary<int, Client> clientsInfo;
    DBManager dBManager;
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
    private void Start() {
        
    }
}
