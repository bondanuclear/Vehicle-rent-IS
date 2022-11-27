using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class IncomeManager : MonoBehaviour
{
    DBManager dBManager;
    UserInfo user;
    float relativeIncome = 0;
    [SerializeField] GameObject parentDailyIncome;
    [SerializeField] GameObject parentMonthlyIncome;
    [SerializeField] GameObject dailyIncomePanel;
    [SerializeField] GameObject monthlyIncomePanel;
    // Start is called before the first frame update
    void Awake()
    {
        user = GetComponent<UserInfo>();
        dBManager = FindObjectOfType<DBManager>();
    }
    private void Start() {
        SpawnMonthlyIncomePanels();
         SpawnDailyIncomePanels();
    }

    private void SpawnMonthlyIncomePanels()
    {
        if (PersistentData.instance.monthlyIncome == null) return;
        //hasSpawned = true;
        
        foreach (var item in PersistentData.instance.monthlyIncome)
        {

            GameObject spawnedObject = Instantiate(monthlyIncomePanel, parentMonthlyIncome.transform);
           
            spawnedObject.name = item.Key.ToString() + "MIncome";
            spawnedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "ID: " + item.Value.incomeID.ToString();
            spawnedObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Value.date;
            spawnedObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "Monthly income: " + item.Value.price.ToString();
            
            // Debug.Log(spawnedObject.name);
            // Debug.Log(item.Key + " VEHICLE ID ");
            

        }
    }
    private void SpawnDailyIncomePanels()
    {
        if (PersistentData.instance.dailyIncome == null) return;
        //hasSpawned = true;
        int index = 0;
        foreach (var item in PersistentData.instance.dailyIncome)
        {
            
            GameObject spawnedObject = Instantiate(dailyIncomePanel, parentDailyIncome.transform);
            float relativeIncome = PersistentData.instance.dailyRelativeIncome.ElementAt(index).Value.price;
            spawnedObject.name = item.Key.ToString() + "Income";
            spawnedObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.Value.date;
            spawnedObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.Value.price.ToString();
            spawnedObject.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = relativeIncome.ToString();
            spawnedObject.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = item.Value.hours.ToString();
            spawnedObject.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = item.Value.vehicleName;
            // Debug.Log(spawnedObject.name);
            // Debug.Log(item.Key + " VEHICLE ID ");
            index++;

        }
    }
    
    public void AddIncomeMovTable(Vehicle vehicle)
    {
         
        Income income = new Income(Maintenance.InvertDate(System.DateTime.Today),
            user.totalPrice, user.rentedHours, vehicle.vehicleName);
        dBManager.AddToIncomeTable(income);
        Income income1 = dBManager.GetLastIncomeRow();
        PersistentData.instance.dailyIncome.Add(income1.incomeID, income1);
    }

    

    public void AddRelativeIncomeTable(Maintenance maintenance)
    {
        relativeIncome = user.totalPrice - (maintenance.powerChargeCost + maintenance.mechServiceCost);
        Debug.Log("RELATIVE INCOME " + relativeIncome );
        Income income = new Income(Maintenance.InvertDate(System.DateTime.Today), relativeIncome, 0, "null");
        dBManager.AddToRelativeIncomeTable(income);
        Income income1 = dBManager.GetLastIncomeRow("RelativeIncome", "rIncomeID");
        Debug.LogWarning("income1 " + income1.incomeID + " " + income.price);
        PersistentData.instance.dailyRelativeIncome.Add(income1.incomeID, income1);

    }
    public void SpawnPanel()
    {
        GameObject objectToSpawn = Instantiate(dailyIncomePanel, parentDailyIncome.transform);
        Income relativeIncome = PersistentData.instance.dailyRelativeIncome.Values.Last();
        Income pureIncome = PersistentData.instance.dailyIncome.Values.Last();
        objectToSpawn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pureIncome.date;
        objectToSpawn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = pureIncome.price.ToString();
        objectToSpawn.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = relativeIncome.price.ToString();
        objectToSpawn.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = pureIncome.hours.ToString();
        objectToSpawn.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = pureIncome.vehicleName;
    }
    public void AddMonthRelativeTable()
    {
        // check the month of last income in month relative table
        Income lastIncome = dBManager.GetLastIncomeRow("MonthRelativeIncome", "mIncomeID");
        Debug.Log(lastIncome.incomeID + " income ID in INCOME MANAGER");
        string todayDate = Maintenance.InvertDate(System.DateTime.Today);
        string lastIncomeMonth = GetLastIncomeMonth(lastIncome.date);
        Debug.Log("Last month is " + lastIncomeMonth);
        
        if(System.DateTime.Today.Month == Convert.ToInt32(lastIncomeMonth))
        {
            float updatedPrice = lastIncome.price + relativeIncome;
            dBManager.UpdateMonthIncome(new Income("", updatedPrice, 0, "null", lastIncome.incomeID));
            PersistentData.instance.monthlyIncome[lastIncome.incomeID] = new Income(
                lastIncome.date, updatedPrice, 0, "null", lastIncome.incomeID);
        } else 
        {
            dBManager.AddToMonthRelativeIncomeTable(new Income(Maintenance.InvertDate(System.DateTime.Today), relativeIncome, 0, "null"));
            Income lastIncome1 = dBManager.GetLastIncomeRow("MonthRelativeIncome", "mIncomeID");
            PersistentData.instance.monthlyIncome.Add(lastIncome1.incomeID, lastIncome1);
            SpawnMonthlyIncomePanel();
        }

    }
    public void SpawnMonthlyIncomePanel()
    {
        GameObject objectToSpawn = Instantiate(dailyIncomePanel, parentDailyIncome.transform);
        Income relativeMonthlyIncome = PersistentData.instance.monthlyIncome.Values.Last();
        
        objectToSpawn.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = relativeMonthlyIncome.incomeID.ToString();
        objectToSpawn.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = relativeMonthlyIncome.date;
        objectToSpawn.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = relativeMonthlyIncome.price.ToString();
       
    }
    private string GetLastIncomeMonth(string stringToSplit)
    {
        string[] date = stringToSplit.Split('-');
        return date[1];
    }
}
