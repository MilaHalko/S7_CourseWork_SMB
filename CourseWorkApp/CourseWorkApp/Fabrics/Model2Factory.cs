using CourseWorkApp.GeneticModels;

namespace CourseWorkApp.Fabrics;

public class Model2Factory : IGeneticModelFactory<Model2>
{
    public Model2 CreateRandomModel()
    {
        var data = new Dictionary<Model2.Names, int>()
        {
            { Model2.Names.DoctorsCount, Random.Shared.Next(1, 5) },
            { Model2.Names.DoctorsQueue, Random.Shared.Next(0, 100) },
            { Model2.Names.AttendantsCount, Random.Shared.Next(1, 5) },
            { Model2.Names.AttendantsQueue, Random.Shared.Next(0, 100) },
            { Model2.Names.RegistryWorkersCount, Random.Shared.Next(1, 5) },
            { Model2.Names.RegistryQueue, Random.Shared.Next(0, 100) },
            { Model2.Names.AssistantsCount, Random.Shared.Next(1, 5) },
            { Model2.Names.AssistantsQueue, Random.Shared.Next(0, 100) },
        };
        return new Model2(data, 3000);
    }
}