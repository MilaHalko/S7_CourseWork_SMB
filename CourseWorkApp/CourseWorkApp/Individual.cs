using CourseWorkApp.GeneticModels;

namespace CourseWorkApp;

public class Individual<T> where T: IGeneticModel
{
    public T GeneticModel { get; set; }
    public double Fitness { get; set; }
    
    public Individual(T geneticModel)
    {
        GeneticModel = geneticModel;
        Fitness = 0;
    }
}