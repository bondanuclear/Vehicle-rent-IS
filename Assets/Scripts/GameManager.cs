using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Button addUserButton;
    DBManager dBManager;
    // Start is called before the first frame update
    void Awake()
    {
        dBManager = GetComponent<DBManager>();
    }
    private void OnEnable() {
        addUserButton.onClick.AddListener(AddUser);
    }
    public void AddUser()
    {
        Vehicle monowheel = new Vehicle(0, "Monowheel", 100, "Electro", 20f, 3);
        Client client = new Client(5,"Qwer", "Tyui", "123456789", monowheel.vehicleID);
        dBManager.AddUserToTable(client);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
