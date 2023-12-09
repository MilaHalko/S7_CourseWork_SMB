using CourseWorkApp.GeneticModels;

namespace CourseWorkApp.Crossovers;

public class Model2Crossover : ICrossoverLogic<Model2>
{
    public Model2 Crossover(Model2 parent1, Model2 parent2)
    {
        Dictionary<Model2.Names, int> childData = new();
        for (var i = 0; i < parent1.Data.Count; i++)
            if (i < parent1.Data.Count / 2)
                childData[Enum.GetValues<Model2.Names>()[i]] = parent1.Data[Enum.GetValues<Model2.Names>()[i]];
            else
                childData[Enum.GetValues<Model2.Names>()[i]] = parent2.Data[Enum.GetValues<Model2.Names>()[i]];

        return new Model2(childData, parent1.SimulationTime);
    }
}