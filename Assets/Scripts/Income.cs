using System.Collections;
using System.Collections.Generic;


public class Income 
{
    public int incomeID {get; private set;}
    public string date {get; private set;}
    public float price {get; private set;}
    public int hours {get; private set;}
    public string vehicleName{get; private set;}
    public Income(string date, float price, int hours, string vehicleName, int incomeID = 0)
    {
        this.incomeID = incomeID;
        this.date = date;
        this.price = price;
        this.hours = hours;
        this.vehicleName = vehicleName;
    }
}
