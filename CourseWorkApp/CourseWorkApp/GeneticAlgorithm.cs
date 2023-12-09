using CourseWorkApp.Crossovers;
using CourseWorkApp.Fabrics;
using CourseWorkApp.GeneticModels;

namespace CourseWorkApp;

class GeneticAlgorithm<T> where T : class, IGeneticModel
{
    private const int PopulationSize = 300;
    private const int MaxGenerations = 50;
    private const double CrossoverProbability = 0.7;
    private const double MutationProbability = 0.3;

    private readonly IGeneticModelFactory<T> _geneticModelFactory;
    private readonly ICrossoverLogic<T> _crossoverLogic;

    public GeneticAlgorithm(IGeneticModelFactory<T> geneticModelFactory, ICrossoverLogic<T> crossoverLogic)
    {
        _geneticModelFactory = geneticModelFactory;
        _crossoverLogic = crossoverLogic;
    }

    public Individual<T> Run()
    {
        var population = InitializePopulation();
        Individual<T> bestIndividual = null;

        for (int generation = 0; generation < MaxGenerations; generation++)
        {
            EvaluatePopulation(population);
            var currentBest = population.OrderByDescending(i => i.Fitness).First();
            Console.WriteLine($"Generation {generation}: Best Fitness = {currentBest.Fitness}");

            if (bestIndividual == null || currentBest.Fitness > bestIndividual.Fitness) bestIndividual = currentBest;

            var selectedParents = TournamentSelection(population);
            population = Crossover(selectedParents);
            Mutate(population);
        }

        Console.WriteLine($"Best Fitness: {bestIndividual.Fitness}");

        return bestIndividual;
    }

    private List<Individual<T>> InitializePopulation()
    {
        var population = new List<Individual<T>>();

        for (var i = 0; i < PopulationSize; i++)
        {
            Individual<T> individual = new(_geneticModelFactory.CreateRandomModel());
            population.Add(individual);
        }

        return population;
    }

    private void EvaluatePopulation(List<Individual<T>> population)
    {
        foreach (var individual in population)
        {
            individual.GeneticModel.Simulate();
            individual.Fitness = individual.GeneticModel.CalculateProfit();
        }
    }

    private List<Individual<T>> TournamentSelection(List<Individual<T>> population)
    {
        var selectedParents = new List<Individual<T>>();

        for (int i = 0; i < PopulationSize; i++)
        {
            var contestant1 = population[Random.Shared.Next(population.Count)];
            var contestant2 = population[Random.Shared.Next(population.Count)];
            var selectedParent = (contestant1.Fitness > contestant2.Fitness) ? contestant1 : contestant2;
            selectedParents.Add(selectedParent);
        }

        return selectedParents;
    }

    private List<Individual<T>> Crossover(List<Individual<T>> parents)
    {
        var offspring = new List<Individual<T>>();

        for (int i = 0; i < PopulationSize; i += 2)
        {
            if (Random.Shared.NextDouble() < CrossoverProbability)
            {
                var parent1 = parents[i];
                var parent2 = parents[i + 1];
                offspring.Add(CreateChildModel(parent1, parent2));
                offspring.Add(CreateChildModel(parent2, parent1));
            }
            else
            {
                offspring.Add(parents[i]);
                offspring.Add(parents[i + 1]);
            }
        }

        return offspring;
    }

    private Individual<T> CreateChildModel(Individual<T> parent1, Individual<T> parent2)
    {
        return new Individual<T>(_crossoverLogic.Crossover(parent1.GeneticModel, parent2.GeneticModel));
    }

    private void Mutate(List<Individual<T>> population)
    {
        foreach (var individual in population)
        {
            if (Random.Shared.NextDouble() < MutationProbability)
            {
                individual.GeneticModel.Mutate();
            }
        }
    }
}