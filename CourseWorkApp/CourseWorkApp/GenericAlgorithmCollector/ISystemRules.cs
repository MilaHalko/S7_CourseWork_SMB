namespace CourseWorkApp.GenericAlgorithmCollector;

public interface ISystemRules
{
    float GetModelFitness(UniversalVector vector);
    bool CheckAlive(UniversalVector vector);
}