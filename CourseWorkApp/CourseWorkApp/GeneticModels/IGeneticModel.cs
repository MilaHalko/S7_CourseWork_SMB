namespace CourseWorkApp.GeneticModels;

public interface IGeneticModel
{
    void Mutate();
    void Simulate();
    double CalculateProfit();
    
}