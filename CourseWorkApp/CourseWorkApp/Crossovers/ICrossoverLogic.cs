using CourseWorkApp.GeneticModels;

namespace CourseWorkApp.Crossovers;

public interface ICrossoverLogic<T> where T : IGeneticModel
{
    T Crossover(T parent1, T parent2);
}