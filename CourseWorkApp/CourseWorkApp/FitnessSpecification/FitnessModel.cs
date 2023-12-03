using MassServiceModeling.Elements;
using MassServiceModeling.Model;

namespace CourseWorkApp.FitnessSpecification;

public class FitnessModel: Model
{
    double _simulationTime;

    public FitnessModel(List<Element> elements, double simulationTime) : base(
        elements, initialStateIsNeeded : false)
    {
        _simulationTime = simulationTime;
    }

    public float SimulateAndGetFitness()
    {
        base.Simulate(_simulationTime, printResult: false);
        return GetFitness();
    }

    // TODO: FitnessModel GetFitness()
    private float GetFitness()
    {
        throw new NotImplementedException();
    }
}