using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static PersistentData instance;
    
    public Dictionary<int, Vehicle> vehiclesInfo;

    public Dictionary<int, Client> clientsInfo;
    private void Awake() {
        if(instance != null)
        {
            return;   
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    
    }
}
