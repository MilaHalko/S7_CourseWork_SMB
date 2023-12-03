namespace CourseWorkApp.GenericAlgorithmCollector;

public class GeneticAlgorithm
{
    GeneticAlgorithmConfig _config;
    private ISystemRules _systemRules;

    public GeneticAlgorithm(GeneticAlgorithmConfig config, ISystemRules systemRules)
    {
        _config = config;
        _systemRules = systemRules;
    }

    public DataVectorModel1 Start(DataVectorModel1 data)
    {
        var currentPopulation = GetFirstPopulation();
        for (var i = 0; i < _config.IterationsMaxNumber; i++)
        {
            var childrenPopulation = GetChildrenPopulation(currentPopulation);
            MutatePopulation(childrenPopulation);
            var fullPopulation = currentPopulation.Concat(childrenPopulation).ToList();
            currentPopulation = SelectBestPopulation(fullPopulation);
        }

        return GetBestElement(currentPopulation);
    }

    private List<DataVectorModel1> GetFirstPopulation()
    {
        var population = new List<DataVectorModel1>();
        while (population.Count < _config.PopulationSize)
        {
            var vector = DataVectorModel1.NewRandomVector();
            if (DataVectorModel1.CheckAlive(vector)) population.Add(vector);
        }

        return population;
    }

    private List<DataVectorModel1> GetChildrenPopulation(List<DataVectorModel1> parents)
    {
        var random = new Random();
        var children = new List<DataVectorModel1>();
        for (var i = 0; i < _config.ChildrenCount; i++)
        {
            var firstParent = parents[random.Next(0, parents.Count)];
            var secondParent = parents[random.Next(0, parents.Count)];
            var child = DataVectorModel1.Crossover(firstParent, secondParent);
            if (DataVectorModel1.CheckAlive(child)) children.Add(child);
            children.Add(child);
        }

        return children;
    }

    private void MutatePopulation(List<DataVectorModel1> population)
    {
        foreach (var element in population) 
            if (Random.Shared.NextDouble() < _config.MutationChance) 
                element.Mutate();
    }

    private List<DataVectorModel1> SelectBestPopulation(List<DataVectorModel1> population)
    {
        var fitness = new Dictionary<DataVectorModel1, float>();
        foreach (var element in population) 
            fitness.Add(element, _systemRules.GetModelFitness(element));
        population.Sort((first, second) => fitness[first].CompareTo(fitness[second]));
        return population.Take(_config.PopulationSize).ToList();
    }

    private DataVectorModel1 GetBestElement(List<DataVectorModel1> sortedPopulation) => sortedPopulation[0]; // Assuming that population is sorted !!!
}