namespace CourseWorkApp.GenericAlgorithm;

public class GenericAlgorithm
{
    private int _firstPopulationSize;
    private int _populationSize;
    
    public GenericAlgorithm(int firstPopulationSize, int populationSize)
    {
        _firstPopulationSize = firstPopulationSize;
        _populationSize = populationSize;
    }

    public void Start(int iterationsMaxNumber, UniversalVector data)
    {
        List<UniversalVector> currentPopulation = GetFirstPopulation();
        for (int i = 0; i < iterationsMaxNumber; i++)
        {
            var crossoverPopulation = GetCrossoverPopulation(currentPopulation);
            MutatePopulation(crossoverPopulation);
            var fullPopulation = currentPopulation.Concat(crossoverPopulation).ToList();
            currentPopulation = SelectBestPopulation(fullPopulation);
        }
    }

    private List<UniversalVector> GetFirstPopulation()
    {
        var population = new List<UniversalVector>();
        while (population.Count < _firstPopulationSize) population.Add(new UniversalVector().Randomize());
        return population;
    }

    private List<UniversalVector> GetCrossoverPopulation(List<UniversalVector> population)
    {
        throw new NotImplementedException();
    }

    private void MutatePopulation(List<UniversalVector> population)
    {
        foreach (var element in population)
        {
            TryToMutate(element);
        }
    }

    private void TryToMutate(object element)
    {
        throw new NotImplementedException();
    }

    private List<UniversalVector> SelectBestPopulation(List<UniversalVector> population)
    {
        population.Sort((a, b) => a.GetFitness().CompareTo(b.GetFitness()));
        return population.Take(_populationSize).ToList();
    }
}