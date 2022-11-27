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
    }
    public void AddRelativeIncomeTable(Maintenance maintenance)
    {
        relativeIncome = user.totalPrice - (maintenance.powerChargeCost + maintenance.mechServiceCost);
        Debug.Log("RELATIVE INCOME " + relativeIncome );
        Income income = new Income(Maintenance.InvertDate(System.DateTime.Today), relativeIncome, 0, "null");
        dBManager.AddToRelativeIncomeTable(income);
    }
    public void AddMonthRelativeTable()
    {
        // check the month of last income in month relative table
        Income lastIncome = dBManager.GetLastIncomeRow();
        Debug.Log(lastIncome.incomeID + " income ID in INCOME MANAGER");
        string todayDate = Maintenance.InvertDate(System.DateTime.Today);
        string lastIncomeMonth = GetLastIncomeMonth(lastIncome.date);
        Debug.Log("Last month is " + lastIncomeMonth);
        
        if(System.DateTime.Today.Month == Convert.ToInt32(lastIncomeMonth))
        {
            float updatedPrice = lastIncome.price + relativeIncome;
            dBManager.UpdateMonthIncome(new Income("", updatedPrice, 0, "null", lastIncome.incomeID));

        } else 
        {
            dBManager.AddToMonthRelativeIncomeTable(new Income(Maintenance.InvertDate(System.DateTime.Today), relativeIncome, 0, "null"));
        }

    }
    private string GetLastIncomeMonth(string stringToSplit)
    {
        string[] date = stringToSplit.Split('-');
        return date[1];
    }
}
