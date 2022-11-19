using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleForm : MonoBehaviour
{
    // void SpawnPanels(Dictionary<int, Vehicle> vehiclesInfo, GameObject panel, GameObject parent)
    // {

    //     if (vehiclesInfo == null) return;

    //     foreach (var item in vehiclesInfo)
    //     {

    //         GameObject spawnedObject = Instantiate(panel, parent.transform);
    //         spawnedObject.name = item.Key.ToString() + "Vehicle";
    //         spawnedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Value.vehicleName;
    //         spawnedObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Value.pricePerHour.ToString() + " UAH/HOUR";
    //         spawnedObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = item.Value.type;
    //         // Debug.Log(spawnedObject.name);
    //         // Debug.Log(item.Key + " VEHICLE ID ");
    //         spawnedObject.GetComponent<Button>().onClick.AddListener(delegate { ChooseVehicle(item.Key); });


    //     }
    // }
    // public void ChooseVehicle(outint vehicleId)
    // {
    //     Debug.Log("Button clicked = " + vehicleId);
    //     this.vehicleId = vehicleId;
    //     Debug.Log(this.vehicleId);
    //     Vehicle vehicle;
    //     vehiclesInfo.TryGetValue(vehicleId, out vehicle);
    //     chosenVehicleText.text = $" {vehicle.vehicleName} ";
    // }
    // public void Return()
    // {
    //     this.vehicleId = null;
    //     chosenVehicleText.text = "";
    // }
}
