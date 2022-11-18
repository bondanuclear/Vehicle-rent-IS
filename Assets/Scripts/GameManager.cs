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
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] TMP_InputField surnameInput;
    [SerializeField] TMP_InputField phoneInput;
    [SerializeField] TextMeshProUGUI warningText;
    DBManager dBManager;
    // Start is called before the first frame update
    void Awake()
    {
        dBManager = GetComponent<DBManager>();
    }
 
    private void OnEnable() {
        //addUserButton.onClick.AddListener(AddUser);
        submitButton.onClick.AddListener(RegisterUser);
    }
    public void RegisterUser()
    {
        if(nameInput.text == "" || surnameInput.text == "" || phoneInput.text == "") 
        {
            Debug.LogWarning("name is Empty"); 
            StartCoroutine(Warning());
            return;
        }
        
        Vehicle monowheel = new Vehicle(0, "Monowheel", 100, "Electro", 20f, 3);
        Client client = new Client(nameInput.text, surnameInput.text, phoneInput.text, monowheel.vehicleID);
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
    void Update()
    {
        
    }
}
