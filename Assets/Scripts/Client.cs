using System.Collections;
using System.Collections.Generic;


public class Client 
{
    public int clientID {get; private set;}
    public string firstName {get; private set;}
    public string surname {get; private set;}
    public string phoneNumber { get; private set; }
    public int VehicleID {get; private set;}
    public Client(int clientID, string firstName, string surname, string phoneNumber, int VehicleID)
    {
        this.clientID = clientID;
        this.firstName = firstName;
        this.surname = surname;
        this.phoneNumber = phoneNumber;
        this.VehicleID = VehicleID;
    }
}
