using System.Collections;
using System.Collections.Generic;


public class Client 
{
    public int clientID {get; private set;}
    public string firstName {get; private set;}
    public string surname {get; private set;}
    public string phoneNumber { get; private set; }
    public int VehicleID {get; private set;}
    public int rentedHours {get; private set;}
    public Client(string firstName, string surname, string phoneNumber, int VehicleID, int rentedHours, int clientID = 0)
    {
        this.clientID = clientID;
        this.firstName = firstName;
        this.surname = surname;
        this.phoneNumber = phoneNumber;
        this.VehicleID = VehicleID;
        this.rentedHours = rentedHours;
    }
}
