using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomeManager : MonoBehaviour
{
    DBManager dBManager;
    UserInfo user;
    float relativeIncome = 0;
    // Start is called before the first frame update
    void Awake()
    {
        user = GetComponent<UserInfo>();
        dBManager = FindObjectOfType<DBManager>();
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
        }

    }
    private string GetLastIncomeMonth(string stringToSplit)
    {
        string[] date = stringToSplit.Split('-');
        return date[1];
    }
}
