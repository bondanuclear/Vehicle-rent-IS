using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class AdminManager : MonoBehaviour
{
    UserInfo userInfo;
    ClientForm clientForm;
    DetailsManager detailsManager;
    DBManager dBManager;
    IncomeManager incomeManager;
    [SerializeField] GameObject[] parents;
    [SerializeField] ScrollRect scrollRect;
    private void Awake() {
        clientForm = GetComponent<ClientForm>();
        userInfo = GetComponent<UserInfo>();
        detailsManager = GetComponent<DetailsManager>();
        dBManager = FindObjectOfType<DBManager>();
        incomeManager = GetComponent<IncomeManager>();
    }
    private void Start() {
       // Income income = dBManager.GetLastIncomeRow();
       // Debug.Log("income " + income.price);
        //dBManager.UpdateMonthIncome(new Income(Maintenance.InvertDate(System.DateTime.Today), 600.456f, 0, "null", income.incomeID));
//         float a = 5.31f;
//         double b = Convert.ToDouble("123,423");
//         string txt = a.ToString(System.Globalization.CultureInfo.InvariantCulture);
//         Debug.Log("txt = " + txt);
//         double a2 = double.Parse(txt, System.Globalization.CultureInfo.InvariantCulture);
//         Debug.Log("a2 = " + a2);
//         System.IFormatProvider cultureUS =
//    new System.Globalization.CultureInfo("en-US");

//         System.Globalization.CultureInfo cultureFr =
//            new System.Globalization.CultureInfo("fr-fr");

//         //...

//         double duration = double.Parse("0.125", cultureUS);
//         double length = double.Parse("25,1415", cultureFr);
//         Debug.Log("duration " + duration);
//         Debug.Log("Length " + length);
        //Debug.Log(float.Parse("3,4444", CultureInfo.InvariantCulture.NumberFormat));
        
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
        Debug.Log(vehicle.vehicleID + " " + vehicle.vehicleName + " " + vehicle.type);
        yield return null;
        // calculate maintenance using vehicle id and details table
        
        Maintenance maintenance = detailsManager.CalculateMaintenance(vehicle);
        dBManager.AddToMaintenanceTable(maintenance);
       
        yield return null;
        incomeManager.AddIncomeMovTable(vehicle);
        incomeManager.AddRelativeIncomeTable(maintenance);
        incomeManager.SpawnPanel();
        yield return null;
        incomeManager.AddMonthRelativeTable();
        // insert into income table and relative income table
        // insert data into month income table
    }
    
    private void TurnOffContents()
    {
        foreach(var item in parents)
        {
            item.SetActive(false);
        }
    }
    public void ShowClients(int index)
    {
        TurnOffContents();
        clientForm.SpawnClientPanels();
        parents[index].SetActive(true);
        scrollRect.content = parents[index].GetComponent<RectTransform>();
    }
    public void TurnContentOn(int index)
    {
        TurnOffContents();
        clientForm.SetClientIDNull();
        parents[index].SetActive(true);
        scrollRect.content = parents[index].GetComponent<RectTransform>();
    }
}
