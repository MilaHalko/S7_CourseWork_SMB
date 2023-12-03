using CourseWorkApp.GenericAlgorithmCollector;

namespace CourseWorkApp.FitnessSpecification;

public class FitnessModelRules : ISystemRules
{
    FitnessModel _model;
    
    public FitnessModelRules(FitnessModel model)
    {
        _model = model;
    }

    public float GetModelFitness(DataVectorModel1 vectorModel1) => _model.SimulateAndGetFitness();
    
}