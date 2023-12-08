using CourseWorkApp.GeneticModels;

namespace CourseWorkApp.Fabrics;

public class Model1Factory : IGeneticModelFactory<Model1>
{
    public Model1 CreateRandomModel()
    {
        var data = new Dictionary<Model1.Names, int>
        {
            { Model1.Names.SubProcessCount1, Random.Shared.Next(1, 30) },
            { Model1.Names.MaxQueue1, Random.Shared.Next(0, 30) },
            { Model1.Names.SubProcessCount2, Random.Shared.Next(1, 30) },
            { Model1.Names.MaxQueue2, Random.Shared.Next(0, 30) },
            { Model1.Names.SubProcessCount3, Random.Shared.Next(1, 30) },
            { Model1.Names.MaxQueue3, Random.Shared.Next(0, 30) },
        };
        return new Model1(data, 1000);
    }
}