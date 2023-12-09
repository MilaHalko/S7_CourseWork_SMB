using CourseWorkApp.GeneticModels;

namespace CourseWorkApp.Fabrics;

public interface IGeneticModelFactory<T> where T: IGeneticModel
{
    T CreateRandomModel();
}