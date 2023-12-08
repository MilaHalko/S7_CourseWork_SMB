using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Elements;

namespace CourseWorkApp.GeneticModels.Model2HospitalElements;

public class LabAssistanceProcess : Process
{
    public LabAssistanceProcess(Randomizer randomizer, int assistanceCount, string name, string subProcessName, int maxQueue = 2147483647) :
        base(randomizer, assistanceCount, name, maxQueue, subProcessName) {}

    protected override void NextElementsContainerSetup()
    {
        if (Item is Client { ClientType: ClientType.NotExamined } client)
        {
            client.ClientType = ClientType.Chamber;
            client.RegistrationTime = 15;
            client.Name = "ChamberClient";
        }
        else NextElementsContainer = null;
    }
}