using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UserInfo : MonoBehaviour
{
    [SerializeField] GameObject estimationPanelParent;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI surnameText;
    [SerializeField] TextMeshProUGUI phoneNumberText;
    [SerializeField] TextMeshProUGUI hoursText;
    [SerializeField] TextMeshProUGUI vehicleNameText;
    [SerializeField] TextMeshProUGUI priceText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator ShowUserInfo(int clientID)
    {
        FillUserInfo(clientID);
        yield return null;
        ProcessUserInfo(true);
    }
    public void ProcessUserInfo(bool isVisible)
    {
        estimationPanelParent.SetActive(isVisible);
    }
    private void FillUserInfo(int clientID)
    {
        Client client;
        PersistentData.instance.clientsInfo.TryGetValue(clientID, out client);
        Vehicle vehicle;
        PersistentData.instance.vehiclesInfo.TryGetValue(client.VehicleID, out vehicle);
        nameText.text = $"Name: {client.firstName}";
        surnameText.text = $"Surname: {client.surname}";
        phoneNumberText.text = $"Phone number: {client.phoneNumber}";
        hoursText.text = $"Hours rented: {client.rentedHours}";
        vehicleNameText.text = $"Vehicle: {vehicle.vehicleName}";
        priceText.text = $"Price: {PersistentData.instance.CalculateFullPrice(vehicle.vehicleID, client.rentedHours)}";
    }
}
