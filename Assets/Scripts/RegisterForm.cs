using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RegisterForm : MonoBehaviour
{
    [SerializeField] TMP_InputField nameInput;
    [SerializeField] TMP_InputField surnameInput;
    [SerializeField] TMP_InputField phoneInput;
    [SerializeField] TextMeshProUGUI warningText;
    DBManager dBManager;
    [SerializeField] Button submitButton; 
    RegisterForm registerForm;
    private void Awake() {
        dBManager = GetComponent<DBManager>();
        registerForm = GetComponent<RegisterForm>();
    }
   

    private void OnEnable()
    {
        //addUserButton.onClick.AddListener(AddUser);
        submitButton.onClick.AddListener(RegisterUser);
        //chooseVehicle.onClick.AddListener(ChooseVehicle);
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
}
