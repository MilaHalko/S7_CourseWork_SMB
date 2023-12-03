using CourseWorkApp;
using CourseWorkApp.FitnessSpecification;
using CourseWorkApp.GenericAlgorithmCollector;

GeneticAlgorithmConfig config = new();
FitnessModelRules rules = new(ModelHelper.GetModel1(DataVectorModel1.NewRandomVector()));
GeneticAlgorithm algorithm = new(config, rules);
var result = algorithm.Start(DataVectorModel1.NewRandomVector());

Console.WriteLine(result);