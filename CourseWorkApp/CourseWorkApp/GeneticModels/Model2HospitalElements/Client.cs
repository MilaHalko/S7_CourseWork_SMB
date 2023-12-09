using MassServiceModeling.Items;

namespace CourseWorkApp.GeneticModels.Model2HospitalElements;

public class Client : Item
{
    public ClientType ClientType { get; set; }
    public double RegistrationTime { get; set; }
    
    public Client(string name, ClientType clientType, double registrationTime, double startTime) : base(startTime)
    {
        Name = name;
        ClientType = clientType;
        RegistrationTime = registrationTime;
    }
}

public enum ClientType
{
    Chamber,
    NotExamined,
    Lab
}