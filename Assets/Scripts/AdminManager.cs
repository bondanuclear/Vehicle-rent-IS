using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminManager : MonoBehaviour
{
    UserInfo userInfo;
    ClientForm clientForm;
    DetailsManager detailsManager;
    DBManager dBManager;
    private void Awake() {
        clientForm = GetComponent<ClientForm>();
        userInfo = GetComponent<UserInfo>();
        detailsManager = GetComponent<DetailsManager>();
        dBManager = FindObjectOfType<DBManager>();
    }

    public void CloseUserInfo()
    {
        userInfo.ProcessUserInfo(false);
    }
    public void EvaluateAndDelete()
    {
        StartCoroutine(ProcessDeleteClient());
    }
    IEnumerator ProcessDeleteClient()
    {
        CloseUserInfo();
        yield return null;
        Vehicle vehicle = clientForm.DeleteClient();
        //Debug.Log(vehicle.vehicleID + " " + vehicle.vehicleName + " " + vehicle.type);
        yield return null;
        // calculate maintenance using vehicle id and details table
        Maintenance maintenance = detailsManager.CalculateMaintenance(vehicle);
        dBManager.AddToMaintenanceTable(maintenance);
        // update mileage
        yield return null;
        // float totalMileage = vehicle.totalMileage + maintenance.mileage;
        // dBManager.UpdateVehicleMileage()
        // insert into maintenance table date + calculated values
    }
    

}
