using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Elements;
using MassServiceModeling.Items;
using MassServiceModeling.NextElement;

namespace CourseWorkApp.GeneticModels.Model2HospitalElements;

public class DoctorProcess : Process
{
    public DoctorProcess(int doctorsCount, string name, string subProcessName, int maxQueue = 2147483647)
        : base(new UniformRandomizer(-5, 5), doctorsCount, name, maxQueue, subProcessName)
    {
        Queue = new ClientsQueue(maxQueue);
    }
    protected override double GetDelay()
    {
        return (Item as Client).RegistrationTime + Randomizer.GenerateDelay();
    }
}

internal class ClientsQueue : ItemsQueue
{
    public ClientsQueue(int limit) : base(limit) {}
    public override Item GetItem()
    {
        var chamberClient = Queue.Find(x => (x as Client).ClientType == ClientType.Chamber);
        if (chamberClient != null)
        { 
            Queue.Remove(chamberClient);
            return chamberClient;
        }
        return base.GetItem();
    }
}

public class NextAfterDoctor : NextElementsContainer
{
    private Process NextAttendantProcess { get; }
    private Process NextWayToLab { get; }
    private DoctorProcess _doctorProcess;

    public NextAfterDoctor(DoctorProcess doctorProcess, Process nextAttendantProcess, Process nextWayToLab)
    {
        _doctorProcess = doctorProcess;
        NextAttendantProcess = nextAttendantProcess;
        NextWayToLab = nextWayToLab;
    }

    protected override Element GetNextElement()
    {
        var client = _doctorProcess.Item as Client;
        return client.ClientType == ClientType.Chamber ? NextAttendantProcess : NextWayToLab;
    }
}