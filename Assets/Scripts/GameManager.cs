using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Button addUserButton;
    [SerializeField] Button submitButton;
    [SerializeField] Button chooseVehicle;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] TMP_InputField surnameInput;
    [SerializeField] TMP_InputField phoneInput;
    [SerializeField] TextMeshProUGUI warningText;
    DBManager dBManager;
    [Header("Vehicle Panel")]
    [SerializeField] GameObject panel = null;
    [SerializeField] GameObject parent = null;
    Dictionary<int, Vehicle> vehiclesInfo;
    int? vehicleId = null;
    // Start is called before the first frame update
    void Awake()
    {
        dBManager = GetComponent<DBManager>();
    }
    private void Start() {
        
        dBManager.FillListWithData(out vehiclesInfo);
        Debug.Log(vehiclesInfo.Count);
        SpawnPanels();
    }
    private void OnEnable() {
        //addUserButton.onClick.AddListener(AddUser);
        submitButton.onClick.AddListener(RegisterUser);
        //chooseVehicle.onClick.AddListener(ChooseVehicle);
    }

    

    public void RegisterUser()
    {
        if(nameInput.text == "" || surnameInput.text == "" || phoneInput.text == "") 
        {
            Debug.LogWarning("name is Empty"); 
            StartCoroutine(Warning());
            return;
        }
        
        //Vehicle monowheel = new Vehicle(0, "Monowheel", 100, "Electro", 20f, 3);
        Client client = new Client(nameInput.text, surnameInput.text, phoneInput.text, 0);
        dBManager.AddUserToTable(client);


        ClearInputFields();
        //Debug.Log(client.firstName  +  "  " + client.surname + "  " + client.phoneNumber);
    }

    private void ClearInputFields()
    {
        nameInput.text = "";
        surnameInput.text = "";
        phoneInput.text = "";
    }

    IEnumerator Warning()
    {
        if(!warningText.gameObject.activeSelf)
        {
            warningText.gameObject.SetActive(true);
            submitButton.GetComponent<Button>().enabled = false;
        }
            
        yield return new WaitForSeconds(2f);
        warningText.gameObject.SetActive(false);
        submitButton.GetComponent<Button>().enabled = true;

    }
    // public void AddUser()
    // {
    //     Client client = new Client(5, "aaa", "ddd", "178489", 430);
    //     dBManager.AddUserToTable(client);
    // }
    // Update is called once per frame
    void SpawnPanels()
    {
        int index = 0;
        if(vehiclesInfo == null) return;
        foreach(var item in vehiclesInfo)
        {
            GameObject spawnedObject  = Instantiate(panel, parent.transform);
            spawnedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Value.vehicleName;
            spawnedObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Value.pricePerHour.ToString() + " UAH/HOUR";
            spawnedObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.Value.type;
            Debug.Log(spawnedObject.name);
            spawnedObject.GetComponent<Button>().onClick.AddListener(delegate{ChooseVehicle(index);});
            index++;
            
        }
    }
    public void ChooseVehicle(int i)
    {
        Debug.Log("Button clicked = " + i);
    }
    
}
