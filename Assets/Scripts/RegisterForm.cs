using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterForm : MonoBehaviour
{
    public int fullPrice = 0;
    [SerializeField] GameObject parentRegisterPanel = null;
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] TMP_InputField surnameInput;
    [SerializeField] TMP_InputField phoneInput;
    [SerializeField] TextMeshProUGUI warningText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] int hours = 2;
    [SerializeField] TMP_Dropdown ddHours;
    [Header("To change the header:")]
    [SerializeField] GameObject RFheader = null;
    [SerializeField] GameObject VFheader = null;
    DBManager dBManager;
    VehicleForm vehicleForm;
    
    [SerializeField] Button submitButton; 
    // delegates
    public delegate void AddClient(Client client);
    public static AddClient addClient;
    private void Awake() {
        dBManager = GetComponent<DBManager>();  
        vehicleForm = GetComponent<VehicleForm>();
    }
   private void Start() {
        //Debug.Log(priceText.text = $"Price: {} UAH";);
        //priceText.text = $"Price: {} UAH";
    }
    private void OnEnable()
    {
        submitButton.onClick.AddListener(RegisterUser);   
        ddHours.onValueChanged.AddListener(delegate{PickHours(ddHours);});
    }
    private void Update() {
        if(vehicleForm.haveChosen)
        {
            vehicleForm.haveChosen = false;
            priceText.text = $"Price: {PersistentData.instance.CalculateFullPrice(vehicleForm.vehicleId.Value, hours)} UAH";
           // priceText.text = $"Price: {vehicleForm.CalculateFullPrice(hours)} UAH";
        }
    }
    public void PickHours(TMP_Dropdown sender)
    {
        string value = sender.options[sender.value].text;
        //Debug.Log(value);
        hours = Convert.ToInt32(value[0].ToString());
        //Debug.Log(hours);
        //fullPrice = vehicleForm.CalculateFullPrice(hours);
        fullPrice = PersistentData.instance.CalculateFullPrice(vehicleForm.vehicleId.Value, hours);
        Debug.Log(fullPrice);
        priceText.text = $"Price: {fullPrice} UAH";
    }
    
    public void RegisterUser()
    {
        if (nameInput.text == "" || surnameInput.text == "" || phoneInput.text == "")
        {
            Debug.LogWarning("name is Empty");
            StartCoroutine(Warning());
            return;
        }

        //Vehicle monowheel = new Vehicle(0, "Monowheel", 100, "Electro", 20f, 3);
        Client client = new Client(nameInput.text, surnameInput.text, phoneInput.text, vehicleForm.vehicleId.Value, hours);
        dBManager.AddUserToTable(client);
        // -
        vehicleForm.DecreaseVehicleAmount();
        // +
        ClearInputFields();
        // + 
        AddClientToDictionary();

        //Debug.Log(client.firstName  +  "  " + client.surname + "  " + client.phoneNumber);
    }

    private void AddClientToDictionary()
    {
        Client client = dBManager.GetLastRow();
        addClient?.Invoke(client);
    }

    private void ClearInputFields()
    {
        nameInput.text = "";
        surnameInput.text = "";
        phoneInput.text = "";
    }

    private IEnumerator Warning()
    {
        if (!warningText.gameObject.activeSelf)
        {
            warningText.gameObject.SetActive(true);
            submitButton.GetComponent<Button>().enabled = false;
        }

        yield return new WaitForSeconds(2f);
        warningText.gameObject.SetActive(false);
        submitButton.GetComponent<Button>().enabled = true;

    }
    public bool ActivateRegForm()
    {
        if(vehicleForm.vehicleId == null) 
        {
            Debug.LogWarning("WARNING, YOU HAVE NOT CHOSEN ANYTHING YET");  
            return false; 
        }
        else
        {
            parentRegisterPanel.SetActive(true);
            vehicleForm.parent.SetActive(false);
            VFheader.SetActive(false);
            RFheader.SetActive(true);
        } 
        return true;
    }
    public void DeactivateRegForm()
    {
        parentRegisterPanel.SetActive(false);
        vehicleForm.parent.SetActive(true);
        VFheader.SetActive(true);
        RFheader.SetActive(false);
    }
}
