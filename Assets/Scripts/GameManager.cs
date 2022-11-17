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
        Client client = new Client(36,"Krake", "Ler", "34511233");
        dBManager.AddUserToTable(client);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
