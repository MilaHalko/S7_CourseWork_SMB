namespace CourseWorkApp.GenericAlgorithmCollector;

public class GenericAlgorithm
{
    private int _populationSize;
    private int _childrenCount;
    private ISystemRules _systemRules;

    public GenericAlgorithm(int populationSize, int childrenCount, ISystemRules systemRules)
    {
        _populationSize = populationSize;
        _childrenCount = childrenCount;
        _systemRules = systemRules;
    }

    public UniversalVector Start(int iterationsMaxNumber, UniversalVector data)
    {
        var currentPopulation = GetFirstPopulation();
        for (var i = 0; i < iterationsMaxNumber; i++)
        {
            var childrenPopulation = GetChildrenPopulation(currentPopulation);
            MutatePopulation(childrenPopulation);
            var fullPopulation = currentPopulation.Concat(childrenPopulation).ToList();
            currentPopulation = SelectBestPopulation(fullPopulation);
        }

        return GetBestElement(currentPopulation);
    }

    private List<UniversalVector> GetFirstPopulation()
    {
        var population = new List<UniversalVector>();
        while (population.Count < _populationSize)
        {
            var vector = new UniversalVector().Randomize();
            if (_systemRules.CheckAlive(vector)) population.Add(vector);
        }

        return population;
    }

    private List<UniversalVector> GetChildrenPopulation(List<UniversalVector> parents)
    {
        var random = new Random();
        var children = new List<UniversalVector>();
        for (var i = 0; i < _childrenCount; i++)
        {
            var firstParent = parents[random.Next(0, parents.Count)];
            var secondParent = parents[random.Next(0, parents.Count)];
            var child = UniversalVector.Crossover(firstParent, secondParent);
            if (_systemRules.CheckAlive(child)) children.Add(child);
            children.Add(child);
        }

        return children;
    }

    private void MutatePopulation(List<UniversalVector> population)
    {
        foreach (var element in population) element.TryToMutate();
    }

    private List<UniversalVector> SelectBestPopulation(List<UniversalVector> population)
    {
        var fitness = new Dictionary<UniversalVector, float>();
        foreach (var element in population) fitness.Add(element, _systemRules.GetModelFitness(element));
        population.Sort((first, second) => fitness[first].CompareTo(fitness[second]));
        return population.Take(_populationSize).ToList();
    }

    private UniversalVector GetBestElement(List<UniversalVector> currentPopulation) => currentPopulation[0]; // Assuming that population is sorted
}