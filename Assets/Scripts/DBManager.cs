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
        
        IDbConnection dbConnection = CreateAndOpenDatabase();
        dbConnection.Close();
    }
    public void AddUserToTable(Client client)
    {
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand  command = dbConnection.CreateCommand();
        //SqliteCommand insertSQL = new SqliteCommand("INSERT INTO Clients (clientID, Firstname, Surname, PhoneNumber) VALUES (35, 'ss', 'sss', '380994455')");
        command.CommandText = $"INSERT INTO Clients(FirstName, Surname, PhoneNumber, VehicleID) VALUES ('{client.firstName}' , '{client.surname}', '{client.phoneNumber}', {client.VehicleID})";
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
        
    }
}
