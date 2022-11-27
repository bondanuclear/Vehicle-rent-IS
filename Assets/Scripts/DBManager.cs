using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;

public class DBManager : MonoBehaviour
{
    [SerializeField] string IS_DB = "URI=file:Clients.sqlite";
    // Start is called before the first frame update
    void Start()
    {
        // string date = Maintenance.InvertDate(DateTime.Today);
        // Debug.Log(date + " DATE ");
        // Maintenance maintenance = new Maintenance(date, 35.5f, 12.7f, 0, 32);
        // AddToMaintenanceTable(maintenance);
        // IDbConnection dbConnection = CreateAndOpenDatabase();
        // dbConnection.Close();
    }
    public void AddUserToTable(Client client)
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand  command = dbConnection.CreateCommand();
        //SqliteCommand insertSQL = new SqliteCommand("INSERT INTO Clients (clientID, Firstname, Surname, PhoneNumber) VALUES (35, 'ss', 'sss', '380994455')");
        command.CommandText = $"INSERT INTO Clients(FirstName, Surname, PhoneNumber, VehicleID, RentedHours) VALUES ('{client.firstName}' , '{client.surname}', '{client.phoneNumber}', {client.VehicleID}, {client.rentedHours})";
        //"INSERT OR REPLACE INTO HitCountTableSimple (id, hits) VALUES (0, " + hitCount + ")"; // 10
        // command.Parameters.Add('3');
        // command.Parameters.Add(client.firstName);
        // command.Parameters.Add(client.surname);
        // command.Parameters.Add(client.phoneNumber);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        dbConnection.Close();
    }   
    private IDbConnection CreateAndOpenDatabase() // 3
    {
        // Open a connection to the database.
         // 4
        IDbConnection dbConnection = new SqliteConnection(IS_DB); // 5
        dbConnection.Open(); // 6
        // Create a table for the hit count in the database if it does not exist yet.
        // IDbCommand dbCommandCreateTable = dbConnection.CreateCommand(); // 6
        // dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS ClientsTableSimple (clientID INTEGER PRIMARY KEY, Firstname TEXT, Surname TEXT, PhoneNumber TEXT )"; // 7
        // dbCommandCreateTable.ExecuteReader(); // 8
        return dbConnection;
    }
    public void FillListWithData(out Dictionary<int, Vehicle> dict)
    {
        dict = new Dictionary<int, Vehicle>();
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM Vehicles";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();
        while (dataReader.Read())
        {
            var id = dataReader.GetInt32(0);
            var name = dataReader.GetString(1);  
            var price = dataReader.GetInt32(2); 
            var type = dataReader.GetString(3); 
            var totalMileage = dataReader.GetFloat(4);      
            var amount = dataReader.GetInt32(5);
            Vehicle vehicle = new Vehicle(id, name, price, type, totalMileage, amount);
            dict.Add(id, vehicle);
        }
        dbConnection.Close();
    }
    public void FillListWithClientsData(out Dictionary<int, Client> dict)
    {
        dict = new Dictionary<int, Client>();
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM Clients";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();
        while (dataReader.Read())
        {
            var id = dataReader.GetInt32(0);
           // Debug.Log(id + "client ID ");
            var name = dataReader.GetString(1);
           // Debug.Log(name + "client Name ");
            var surname = dataReader.GetString(2);
           // Debug.Log(surname + "client SURNAME ");
            var phoneNumber = dataReader.GetString(3);
           // Debug.Log(phoneNumber + "client phoneNumber ");
            var vehicleID = dataReader.GetInt32(4);
           // Debug.Log(vehicleID + "client vehicleID ");
            var rentedHours = dataReader.GetInt32(5);
            //Debug.Log(rentedHours + "client rentedHours ");
            Client client = new Client(name, surname, phoneNumber, vehicleID, rentedHours, id);
            dict.Add(id, client);
        }
        dbConnection.Close();
    }
    public void FillDetailsListWithData(out Dictionary<int, Details> dict)
    {
        dict = new Dictionary<int, Details>();
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM VehiclesDetails";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();
        while (dataReader.Read())
        {
            var id = dataReader.GetInt32(0);
            //Debug.Log(id + "details ID ");
            var vehicleID = dataReader.GetInt32(1);
            //Debug.Log(vehicleID + " vehicleID ");
            var maxDistance = dataReader.GetInt32(2);
            //Debug.Log(maxDistance + " maxDistance ");
            var batteryWatt = dataReader.GetInt32(3);
            //Debug.Log(batteryWatt + " batteryWatt ");
            var averageSpeed = dataReader.GetFloat(4);
            // Debug.Log(vehicleID + "client vehicleID ");
            var hoursToCharge = dataReader.GetFloat(5);
            //Debug.Log(rentedHours + "client rentedHours ");
            Details details = new Details(id, vehicleID, maxDistance, batteryWatt, averageSpeed, hoursToCharge);
            dict.Add(vehicleID, details);
        }
        dbConnection.Close();
    }
    public void UpdateVehicleAmount(Vehicle vehicle)
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = $"UPDATE Vehicles SET amount = {vehicle.amount} where vehicleID = {vehicle.vehicleID}";
        try
        {
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        dbConnection.Close();
    }
    // обновити пробіг певного транспорту
    public void UpdateVehicleMileage(Vehicle vehicle)
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = $"UPDATE Vehicles SET totalMileage = {vehicle.totalMileage} where vehicleID = {vehicle.vehicleID}";
        try
        {
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        dbConnection.Close();
    }
    public Client GetLastRow()
    {
        Client client = null;
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM Clients ORDER BY clientID DESC LIMIT 1;";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();
        while (dataReader.Read())
        {
            var id = dataReader.GetInt32(0);
            //Debug.Log(id + "client ID ");
            var name = dataReader.GetString(1);
            //Debug.Log(name + "client Name ");
            var surname = dataReader.GetString(2);
            //Debug.Log(surname + "client SURNAME ");
            var phoneNumber = dataReader.GetString(3);
            //Debug.Log(phoneNumber + "client phoneNumber ");
            var vehicleID = dataReader.GetInt32(4);
            // Debug.Log(vehicleID + "client vehicleID ");
            var rentedHours = dataReader.GetInt32(5);
            // Debug.Log(rentedHours + "client rentedHours ");
            client = new Client(name, surname, phoneNumber, vehicleID, rentedHours, id);

        }
        dbConnection.Close();
        return client;
    }
    public void DeleteClientFromTable(int clientID)
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = $"DELETE FROM Clients WHERE clientID = {clientID}";
        try
        {
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        dbConnection.Close();
    }
    public void AddToMaintenanceTable(Maintenance maintenance)
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand command = dbConnection.CreateCommand();
        string powerCharge = maintenance.powerChargeCost.ToString(System.Globalization.CultureInfo.InvariantCulture);
        string mechService = maintenance.mechServiceCost.ToString(System.Globalization.CultureInfo.InvariantCulture);
        Debug.Log(maintenance.date + " " + maintenance.powerChargeCost + " " + maintenance.mechServiceCost);
        command.CommandText = $"INSERT INTO Maintenance_DAY(date, powerChargeCost, mechService, vehicleID, mileage) VALUES ('{maintenance.date}' , '{powerCharge}', '{mechService}' , {maintenance.vehicleID}, '{maintenance.mileage}')";
        try
        {
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        dbConnection.Close();
    }
    public void AddToIncomeTable(Income income)
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand command = dbConnection.CreateCommand();
        command.CommandText = $"INSERT INTO IncomeMov(date, price, hours, vehicleName) VALUES ('{income.date}', '{income.price}' , {income.hours}, '{income.vehicleName}')";
        try
        {
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        dbConnection.Close();
    }
    public void AddToRelativeIncomeTable(Income income)
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand command = dbConnection.CreateCommand();
        string incomePrice = income.price.ToString(System.Globalization.CultureInfo.InvariantCulture);
        command.CommandText = $"INSERT INTO RelativeIncome(date, relativeIncome) VALUES ('{income.date}', '{incomePrice}')";
        try
        {
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        dbConnection.Close();
    }
    public void AddToMonthRelativeIncomeTable(Income income)
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand command = dbConnection.CreateCommand();
        string incomePrice = income.price.ToString(System.Globalization.CultureInfo.InvariantCulture);
        command.CommandText = $"INSERT INTO MonthRelativeIncome(date, monthIncome) VALUES ('{income.date}', '{incomePrice}')";
        try
        {
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        dbConnection.Close();
    }
    public void UpdateMonthIncome(Income income)
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand command = dbConnection.CreateCommand();
        string incomePrice = income.price.ToString(System.Globalization.CultureInfo.InvariantCulture);
        command.CommandText = $"UPDATE MonthRelativeIncome SET monthIncome = '{incomePrice}' where mIncomeID = {income.incomeID}";
        try
        {
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        dbConnection.Close();
    }
    public Income GetLastIncomeRow()
    {
        Income income = null;
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = $"SELECT * FROM IncomeMov ORDER BY IncomeID DESC LIMIT 1;";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();
        while (dataReader.Read())
        {
            var id = dataReader.GetInt32(0);
            Debug.Log(id + "income ID ");
            var date = dataReader.GetString(1);
            Debug.Log(date + " date ");
            var price = dataReader.GetFloat(2);
            var hours = dataReader.GetInt32(3);
            var vehicleName = dataReader.GetString(4);
            income = new Income(date, price, hours, vehicleName, id);
        }
        dbConnection.Close();
        return income;
    }
    public Income GetLastIncomeRow(string table, string orderID)
    {
        Income income = null;
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = $"SELECT * FROM {table} ORDER BY {orderID} DESC LIMIT 1;";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();
        while (dataReader.Read())
        {
            var id = dataReader.GetInt32(0);
            Debug.Log(id + "relative income ID ");
            var date = dataReader.GetString(1);
            Debug.Log(date + " date ");
            var stringPrice = dataReader.GetString(2);
            Debug.Log(stringPrice + " price ");
            float price = (float) double.Parse(stringPrice, System.Globalization.CultureInfo.InvariantCulture);
            income = new Income(date, price, 0, "null", id);
        }
        dbConnection.Close();
        return income;
    }
    public void FillIncomeData(out Dictionary<int, Income> dailyIncome)
    {
        dailyIncome = new Dictionary<int, Income>();
        
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM IncomeMov";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();
        while (dataReader.Read())
        {
            var id = dataReader.GetInt32(0);
            //Debug.Log(id + "details ID ");
            var date = dataReader.GetString(1);
            //Debug.Log(vehicleID + " vehicleID ");
            var price = dataReader.GetFloat(2);
            //Debug.Log(maxDistance + " maxDistance ");
            var hours = dataReader.GetInt32(3);
            //Debug.Log(batteryWatt + " batteryWatt ");
            var vehicleName = dataReader.GetString(4);
            // Debug.Log(vehicleID + "client vehicleID ");
            
            Income income = new Income(date, price, hours, vehicleName, id);
            dailyIncome.Add(id, income);
        }
        dbConnection.Close();
    }
    public void FillRelativeIncomeData(out Dictionary<int, Income> relativeIncome)
    {
        relativeIncome = new Dictionary<int, Income>();

        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM RelativeIncome";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();
        while (dataReader.Read())
        {
            var id = dataReader.GetInt32(0);
            //Debug.Log(id + "details ID ");
            var date = dataReader.GetString(1);
            //Debug.Log(vehicleID + " vehicleID ");
            var stringPrice = dataReader.GetString(2);
            float price = (float)double.Parse(stringPrice, System.Globalization.CultureInfo.InvariantCulture);
            

            Income income = new Income(date, price, 0, "null", id);
            relativeIncome.Add(id, income);
        }
        dbConnection.Close();
    }
    public void FillMonthlyIncomeData(out Dictionary<int, Income> monthlyIncome)
    {
        monthlyIncome = new Dictionary<int, Income>();

        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM MonthRelativeIncome";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();
        while (dataReader.Read())
        {
            var id = dataReader.GetInt32(0);
            //Debug.Log(id + "details ID ");
            var date = dataReader.GetString(1);
            //Debug.Log(vehicleID + " vehicleID ");
            var stringPrice = dataReader.GetString(2);
            float price = (float)double.Parse(stringPrice, System.Globalization.CultureInfo.InvariantCulture);


            Income income = new Income(date, price, 0, "null", id);
            monthlyIncome.Add(id, income);
        }
        dbConnection.Close();
    }
}
