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
    [SerializeField] int hours = 2;
    [SerializeField] TMP_Dropdown ddHours;
    [Header("To change the header:")]
    [SerializeField] GameObject RFheader = null;
    [SerializeField] GameObject VFheader = null;
    DBManager dBManager;
    VehicleForm vehicleForm;
    [SerializeField] Button submitButton; 
    
    private void Awake() {
        dBManager = GetComponent<DBManager>();  
        vehicleForm = GetComponent<VehicleForm>();
    }
   
    private void OnEnable()
    {
        submitButton.onClick.AddListener(RegisterUser);   
        ddHours.onValueChanged.AddListener(delegate{PickHours(ddHours);});
    }

    public void PickHours(TMP_Dropdown sender)
    {
        string value = sender.options[sender.value].text;
        //Debug.Log(value);
        hours = Convert.ToInt32(value[0].ToString());
        //Debug.Log(hours);
        fullPrice = vehicleForm.CalculateFullPrice(hours);
        Debug.Log(fullPrice);
        
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
        vehicleForm.DecreaseVehicleAmount();
        
        ClearInputFields();
        //Debug.Log(client.firstName  +  "  " + client.surname + "  " + client.phoneNumber);
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
