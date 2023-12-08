using DistributionRandomizer.DelayRandomizers;
using MassServiceModeling.Elements;
using MassServiceModeling.Items;
using MassServiceModeling.TimeClasses;

namespace CourseWorkApp.GeneticModels.Model2HospitalElements;

public class CreateClient : Create
{
    public CreateClient(Randomizer randomizer, string name) : base(randomizer, name) {}

    protected override Item CreateItem()
    {
        var number = new Random().NextDouble();
        if (number <= 0.5) return new Client("ChamberClient", ClientType.Chamber, 15, Time.Curr);
        if (number <= 0.6) return new Client("NotExaminedChamberClient", ClientType.NotExamined, 40, Time.Curr);
        return new Client("LabClient", ClientType.Lab, 30, Time.Curr);
    }
}